using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPotionObject : PotionInteractionObject {
	public override IEnumerator PotionEffects() {
		GetComponent<Rigidbody>().isKinematic = false;
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}
}
