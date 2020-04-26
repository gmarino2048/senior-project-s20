using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Visuals
{
    public class Flicker : MonoBehaviour
    {
        public int randomSeed = 2000;
        public int numberPrimes = 20;
        public float flickerSpeed = 1.0f;
        public float maxFlickerDuration = 0.5f;

        private System.Random _random;

        private List<Light> _campfireLights;
        private List<float> _lightIntensity;
        private List<int> _primes;
        private List<FlickerInterval> _intervals;

        private List<float> _nextFlicker;

        class FlickerInterval
        {
            private Light light { get; }
            private float interval { get; }
            private float counter { get; set; }

            
            private readonly float _maxFlickerDuration;
            private readonly Random _random;
            private const float rampTime = 0.05f;

            public FlickerInterval(Light light, float interval, float maxFlickerDuration, Random random = null)
            {
                this.light = light;
                this.interval = interval;
                counter = 0;

                _maxFlickerDuration = maxFlickerDuration;
                _random = random ?? new Random();
            }

            public IEnumerator Increment(float deltaTime)
            {
                counter += deltaTime;
                if (counter > interval)
                {
                    counter = 0;
                    yield return DoFlicker();
                }
            }

            private IEnumerator DoFlicker()
            {
                var duration = _maxFlickerDuration * (float)_random.NextDouble();

                var currentIntensity = light.intensity;
                var finalIntensity = currentIntensity  * 0.75f;
                
                yield return RampDown(currentIntensity, finalIntensity);
                yield return new WaitForSeconds(duration);
                yield return RampUp(finalIntensity, currentIntensity);

                // Need to set this so because the scene just keeps getting dimmer and dimmer
                light.intensity = currentIntensity;
            }

            private IEnumerator RampDown(float initialIntensity, float finalIntensity)
            {
                if (initialIntensity < finalIntensity)
                {
                    yield return RampUp(initialIntensity, finalIntensity);
                }
                else
                {
                    var currentIntensity = initialIntensity;
                    while (currentIntensity > finalIntensity)
                    {
                        var span = initialIntensity - finalIntensity;
                        var decAmount = span * (Time.deltaTime / rampTime);

                        light.intensity -= decAmount;
                        currentIntensity -= decAmount;
                        yield return new WaitForEndOfFrame();
                    }
                }
            }

            private IEnumerator RampUp(float initialIntensity, float finalIntensity)
            {
                if (initialIntensity > finalIntensity)
                {
                    yield return RampDown(initialIntensity, finalIntensity);
                }
                else
                {
                    var currentIntensity = initialIntensity;
                    while (currentIntensity < finalIntensity)
                    {
                        var span = finalIntensity - currentIntensity;
                        var incAmount = span * (Time.deltaTime / rampTime);

                        light.intensity += incAmount;
                        currentIntensity += incAmount;
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _random = new System.Random(randomSeed);
            _campfireLights = new List<Light>(GetCampfireLights());
            
            foreach (var light in _campfireLights)
            {
                _lightIntensity.Add(light.intensity);
            }

            // Avoid light flicker overlapping
            if (numberPrimes < _campfireLights.Count * 2)
                numberPrimes = _campfireLights.Count * 2;
            
            _primes = new List<int>(GetPrimes(numberPrimes));

            // Select primes from the list for each light
            _intervals = new List<FlickerInterval>();
            foreach (var campfireLight in _campfireLights)
            {
                var interval = GetInterval(_primes, _random);
                _intervals.Add(new FlickerInterval(
                    campfireLight,
                    interval,
                    maxFlickerDuration,
                    _random));
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var flickerInterval in _intervals)
            {
                StartCoroutine(flickerInterval.Increment(Time.deltaTime * flickerSpeed));
            }
        }

        public void ResetIntensity()
        {
            StopAllCoroutines();
            
            for (var i = 0; i < _campfireLights.Count; i++)
            {
                var light = _campfireLights[i];
                light.intensity = _lightIntensity[i];
            }
        }
        
        // Get the interval for each light
        private float GetInterval(List<int> primes, Random random = null)
        {
            random = random ?? new Random();
            var selected = new SortedSet<int>(RandomSelect(2, primes, random));
            
            // List of primes should be unique so standard removal should work
            foreach (var item in selected)
            {
                primes.Remove(item);
            }

            return selected.Max / (float) selected.Min;
        }

        // Fetch all the point lights in the object
        private IEnumerable<Light> GetCampfireLights()
        {
            return gameObject.GetComponentsInChildren<Light>().Where(
                light => light.type == LightType.Point);
        }

        // Get a list of all the intervals that shouldn't overlap
        private IEnumerable<int> GetPrimes(int numPrimes)
        {
            int count;
            var primeList = new List<int>();

            int nextPrime = 3;
            while ((count = primeList.Count) <= numPrimes)
            {
                switch (count)
                {
                    case 0:
                        primeList.Add(2);
                        break;
                    case 1:
                        primeList.Add(3);
                        break;
                    default:
                        if (IsPrime(nextPrime, primeList))
                        {
                            primeList.Add(nextPrime);
                        }

                        break;
                }

                nextPrime += 2;
            }

            return primeList;
        }

        // Check to see if a number is prime given a list of other primes
        private bool IsPrime(int candidate, IEnumerable<int> previousValues)
        {
            var sqrt = (int) Math.Sqrt(candidate);

            foreach (var prime in previousValues)
            {
                if (prime > sqrt) return true;
                if (candidate % prime == 0) return false;
            }

            return true;
        }

        // Select `count` elements randomly from list
        private IEnumerable<T> RandomSelect<T>(int count, IList<T> list, System.Random random = null)
        {
            var retVal = new List<T>(count);
            var randomImpl = random ?? new System.Random();

            for (var i = 0; i < count; i++)
            {
                var idx = random.Next(list.Count());
                retVal.Add(list[idx]);
                list.RemoveAt(idx);
            }

            return retVal;
        }
    }
}
