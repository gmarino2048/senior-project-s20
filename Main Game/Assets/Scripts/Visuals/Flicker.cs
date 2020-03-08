using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public int RandomSeed { get; set; }
    public int NumberPrimes { get; set; }
    public float FlickerSpeed { get; set; }

    private System.Random random;

    private List<Light> campfireLights;
    private List<int> primes;

    private List<float> nextFlicker;
    

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(RandomSeed);

        campfireLights = new List<Light>(GetCampfireLights());
        primes = new List<int>(GetPrimes(NumberPrimes));

        // Select primes from the list for each light
        var selectedPrimes = RandomSelect<int>(campfireLights.Count(), primes, random);
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
        while((count = primeList.Count) <= numPrimes)
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
                    if(IsPrime(nextPrime, primeList))
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
        var sqrt = (int)Math.Sqrt(candidate);

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
