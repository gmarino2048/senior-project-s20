using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreehouseWindow : MonoBehaviour {
	[SerializeField] private float openDuration;
	//HEY WHOEVER IS MAKING THE CAMPFIRE SCENE PUT A HINGE ON THE TINY WINDOW AND PUT IT HERE:
	[SerializeField] private Transform campSceneWindow;

	public void OpenWindow() {
		StartCoroutine(OpenWindowCoroutine());
	}

	private IEnumerator OpenWindowCoroutine() {
		float timer = 0;
		while (timer < openDuration) {
			timer += Time.deltaTime;
			transform.Rotate(new Vector3(0, 0, (Time.deltaTime / openDuration) * -100));
			yield return new WaitForEndOfFrame();
		}

		campSceneWindow.Rotate(new Vector3(0, 0, -100));
	}
}
