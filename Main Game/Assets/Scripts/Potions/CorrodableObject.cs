using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrodableObject : PotionInteractionObject {
	[SerializeField] ParticleSystem[] CorrosionParticles;

	void Start() {
		requiredEffect = PotionType.Acid;
	}

	public override IEnumerator PotionEffects() {
		float partSysDuration = CorrosionParticles[0].main.duration;

		foreach (ParticleSystem ps in CorrosionParticles)
			ps.Play();
		Collider myCollider = GetComponent<Collider>();
		if (myCollider != null)
			myCollider.enabled = false;
		yield return new WaitForSeconds(partSysDuration);
		this.gameObject.SetActive(false);
	}
}
