using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public int NumberPrimes { get; set; }
    public float FlickerSpeed { get; set; }

    private IEnumerable<Light> campfireLights;
    private IEnumerable<int> primes;
    

    // Start is called before the first frame update
    void Start()
    {
        campfireLights = GetCampfireLights();
        primes = GetPrimes(NumberPrimes);
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

    private bool Within(float comp, float target, float tolerance)
    {
        var upper = target + tolerance;
        var lower = target - tolerance;

        return lower <= comp && comp <= upper;
    }
}
