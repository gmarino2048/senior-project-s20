using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Visuals
{
    public class Flicker : MonoBehaviour
    {
        public int randomSeed { get; set; }
        public int numberPrimes { get; set; }
        public float flickerSpeed { get; set; }

        private System.Random _random;

        private List<Light> _campfireLights;
        private List<int> _primes;

        private List<float> _nextFlicker;


        // Start is called before the first frame update
        void Start()
        {
            _random = new System.Random(randomSeed);

            _campfireLights = new List<Light>(GetCampfireLights());
            _primes = new List<int>(GetPrimes(numberPrimes));

            // Select primes from the list for each light
            var selectedPrimes = RandomSelect<int>(
                _campfireLights.Count() * 2,
                _primes,
                _random);
        }

        // Update is called once per frame
        void Update()
        {

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

        // Check whether a float is within a given tolerance range
        // More stable than a direct float comparison
        private bool Within(float comp, float target, float tolerance)
        {
            var upper = target + tolerance;
            var lower = target - tolerance;

            return lower <= comp && comp <= upper;
        }

        private IEnumerable<T> RandomSelect<T>(int count, IList<T> list, System.Random random = null)
        {
            var retVal = new List<T>(count);
            var randomImpl = random ?? new System.Random();

            for (var i = 0; i < count; i++)
            {
                int idx = random.Next(list.Count());
                retVal[i] = list[idx];
                list.RemoveAt(idx);
            }

            return retVal;
        }
    }
}
