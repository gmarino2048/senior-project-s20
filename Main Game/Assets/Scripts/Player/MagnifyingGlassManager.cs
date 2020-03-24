using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagnifyingGlassManager : MonoBehaviour {
	[SerializeField] GameObject magnifyingGlassObject;
	private bool isEnabled;

	[SerializeField] private float rotationDuration;
	private bool isAnimating = false;

	void Update() {
		if (isEnabled) {
			//do things here to raycast forward and jump into scenes
		}
	}

	public void Toggle() {
		if (!isAnimating) {
			isAnimating = true;

			StartCoroutine(ToggleAnimation(!isEnabled));
		}
	}

	private IEnumerator ToggleAnimation(bool doEnable) {
		float timer = 0;
		int direction;
		isAnimating = true;

		if (doEnable) {
			magnifyingGlassObject.SetActive(true);
			direction = -1;
		}
		else {
			direction = 1;
		}
			
		while (timer < rotationDuration) {
			transform.Rotate(new Vector3(0, 0, Time.deltaTime * (90 / rotationDuration) * direction));
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		isEnabled = doEnable;
		//The rotation above is slightly imprecise, so this resets it to the exact angle we want
		if (doEnable) {
			transform.rotation = new Quaternion();
			transform.Rotate(new Vector3(0, 0, 180));
		}
		else {
			magnifyingGlassObject.SetActive(false);
		}

		isAnimating = false;
	}
}
