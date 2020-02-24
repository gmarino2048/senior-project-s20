using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate{};

    private bool wheelIsSpinning;
    private int currentNumber;
    
    void Start()
    {
        wheelIsSpinning = false;
        currentNumber = 0;
    }

    private void OnMouseDown(){
        if(!wheelIsSpinning){
            StartCoroutine("RotateWheel");
        }
    }

    private IEnumerator RotateWheel(){
        wheelIsSpinning = true;

        for (int i = 0; i < 15; i++){
            transform.Rotate(0f, -3f, 0f);
            yield return new WaitForSeconds(0.01f);
        }

        wheelIsSpinning = false;
        currentNumber++;

        if (currentNumber > 7){
            currentNumber = 0;
        }

        Rotated(name, currentNumber);
    }
}
