using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBEdgeScroller : MonoBehaviour {
	[SerializeField] private float scrollSpeed;
	[SerializeField] private GameObject gameScene;
	private bool scrolling;

	private void Update() {
		if (scrolling) {
			gameScene.transform.Translate(new Vector3(-scrollSpeed * Time.deltaTime, 0, 0));
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.GetComponent<GBPlayerController>() != null) {
			scrolling = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.GetComponent<GBPlayerController>() != null) {
			scrolling = false;
		}
	}
}
