﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceberg : PotionInteractionObject {
	[SerializeField] private float growthDuration, meltDuration;
	private Vector3 originalScale;

	void Start() {
		transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
		originalScale = transform.localScale;
		transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		StartCoroutine(Grow());
	}

	private IEnumerator Grow() {
		float timer = 0;
		while(timer < growthDuration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timer / growthDuration);
			yield return new WaitForEndOfFrame();
		}
	}

	public override IEnumerator PotionEffects() {
		float timer = 0;
		while (timer < meltDuration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timer / meltDuration);
			yield return new WaitForEndOfFrame();
		}
		Destroy(this.gameObject);
	}
}
