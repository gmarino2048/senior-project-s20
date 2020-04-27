using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBEdgeScroller : MonoBehaviour {
	private float scrollSpeed;
	[SerializeField] private float scrollingWidth;
	private float idleWidth;
	[SerializeField] private GameObject gameScene;
	private bool scrolling;

	private void Start() {
		idleWidth = transform.localScale.x;
		GBPlayerController player = FindObjectOfType<GBPlayerController>();
		scrollSpeed = player.moveSpeed;
	}

	private void FixedUpdate() {
		if (scrolling) {
			gameScene.transform.Translate(new Vector2(-scrollSpeed * Time.deltaTime, 0));
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.GetComponent<GBPlayerController>() != null) {
			transform.localScale = new Vector2(scrollingWidth, 2);
			scrolling = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.GetComponent<GBPlayerController>() != null) {
			transform.localScale = new Vector2(idleWidth, 2);
			scrolling = false;
		}
	}

	public void Respawn() {
		gameScene.transform.localPosition = Vector2.zero;
	}
}
