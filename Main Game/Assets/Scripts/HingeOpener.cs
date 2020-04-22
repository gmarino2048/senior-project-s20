using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeOpener : MonoBehaviour {
	public float openDuration;
	public float openDegrees;
	[SerializeField] private Transform exteriorHinge;

	public void Open() {
		print("run");
		StartCoroutine(OpenCoroutine());
	}

	private IEnumerator OpenCoroutine() {
		float timer = 0;
		while (timer < openDuration) {
			timer += Time.deltaTime;
			transform.Rotate(new Vector3(0, 0, (Time.deltaTime / openDuration) * openDegrees));
			yield return new WaitForEndOfFrame();
		}
		if(exteriorHinge != null)
			exteriorHinge.Rotate(new Vector3(0, 0, openDegrees));
	}
}
