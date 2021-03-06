﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoButton : InteractiveObject {
	[SerializeField] private GameObject ball;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float pressTime;
	[SerializeField] private float pressSpeed;
	private bool isPressing;

	public override void Interact(Transform handTF) {
		if (!isPressing)
			StartCoroutine(PressButton());
	}

	private IEnumerator PressButton() {
		float timer = 0;
		Vector3 originalPos = transform.localPosition;

		isPressing = true;
		while(timer < pressTime) {
			timer += Time.deltaTime;
			transform.Translate(new Vector3(0, -pressSpeed * Time.deltaTime, 0));
			yield return new WaitForEndOfFrame();
		}

		SpawnBall();

		while (timer > 0) {
			timer -= Time.deltaTime;
			transform.Translate(new Vector3(0, pressSpeed * Time.deltaTime, 0));
			yield return new WaitForEndOfFrame();
		}
		isPressing = false;
		transform.localPosition = originalPos;
	}

	public void SpawnBall() {
		Instantiate(ball, spawnPoint.position, new Quaternion());
	}
}
