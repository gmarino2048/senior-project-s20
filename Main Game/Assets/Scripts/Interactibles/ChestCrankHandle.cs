using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrankHandle : InteractiveObject {
	[SerializeField] private float turnSpeed;
	[SerializeField] private HingeOpener chestHinge;

	public override void Interact(Transform handTF) {
		GetComponent<Collider>().enabled = false;
		chestHinge.Open();
		StartCoroutine(Spin());
	}

	private IEnumerator Spin() {
		float timer = chestHinge.openDuration;
		while(timer > 0) {
			timer -= Time.deltaTime;
			transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0));
			yield return new WaitForEndOfFrame();
		}
	}
}
