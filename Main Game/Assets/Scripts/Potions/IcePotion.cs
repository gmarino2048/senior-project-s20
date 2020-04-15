using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePotion : PotionItem {
	[SerializeField] private GameObject iceberg;

	protected override void Start() {
		base.Start();
		effectType = PotionType.Ice;
}

	protected override void OnCollisionEnter(Collision other) {
		if (hasBeenThrown) {
			hasBeenThrown = false;
			StartCoroutine(Shatter());
			if(other.gameObject.tag == "Water") {
				GameObject ice = Instantiate(iceberg, transform.position, Quaternion.identity);
			}
		}
	}

	protected override IEnumerator Shatter() {
		particles.Play();
		yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
		Destroy(this.gameObject);
	}
}
