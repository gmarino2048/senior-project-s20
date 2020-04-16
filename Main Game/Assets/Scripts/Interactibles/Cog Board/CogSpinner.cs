using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogSpinner : MonoBehaviour {
	[SerializeField] private float rotationSpeed;
	private bool isSpinning;

	public void StartSpinning() {
		isSpinning = true;
	}

	void Update() {
		if(isSpinning)
			transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
	}
}
