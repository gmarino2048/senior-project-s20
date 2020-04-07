using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wheel : MonoBehaviour
{
	public static event Action<int, int> Rotated = delegate{};

	private bool wheelIsSpinning;
	private int currentNumber;
	
	void Start()
	{
		wheelIsSpinning = false;
		currentNumber = 0;
	}

	private void OnMouseDown(){
		Debug.Log("Clicking");
		if(!wheelIsSpinning){
			StartCoroutine(RotateWheel());
		}
	}

	private IEnumerator RotateWheel(){
		wheelIsSpinning = true;

		for (int i = 0; i < 15; i++){
			transform.Rotate(0f, 0f, -3f);
			yield return new WaitForSeconds(0.01f);
		}

		wheelIsSpinning = false;
		currentNumber++;

		if (currentNumber > 7){
			currentNumber = 0;
		}

		int wheel = int.Parse(name.Substring(name.Length - 1));

		Rotated(wheel, currentNumber);
	}
}
