using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrankHandle : HoldableObject {
	public bool isAttached;
	[SerializeField] private float turnSpeed;
	[SerializeField] private HingeOpener chestHinge;

	public override void Interact(Transform handTF) {
		if (isAttached) {
			GetComponent<Collider>().enabled = false;
			chestHinge.Open();
			StartCoroutine(Spin());
		}
		else {
			base.Interact(handTF);
		}
	}

	private IEnumerator Spin() {
		float timer = chestHinge.openDuration;
		while(timer > 0) {
			timer -= Time.deltaTime;
			transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime));
			yield return new WaitForEndOfFrame();
		}
	}
}
