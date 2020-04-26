using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrankStandalone : InteractiveObject {
	[SerializeField] private float turnSpeed;
	[SerializeField] private HingeOpener chestHinge;
	[SerializeField] private GameObject crankHandle;
	private bool isCranking;

	public override void Interact(Transform handTF) {
		if (!isCranking) {
			isCranking = true;
			chestHinge.Open();
			StartCoroutine(Spin());
		}
	}

	private IEnumerator Spin() {
		float timer = chestHinge.openDuration;
		while(timer > 0) {
			timer -= Time.deltaTime;
			crankHandle.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0), Space.Self);
			yield return new WaitForEndOfFrame();
		}
	}
}
