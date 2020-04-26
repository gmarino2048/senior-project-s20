using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : PotionInteractionObject {
	[SerializeField] ParticleSystem[] explosionParticles;

	void Start() {
		requiredEffect = PotionType.Explosive;
	}

	public override IEnumerator PotionEffects()
	{
		this.gameObject.SetActive(false);
		float partSysDuration = explosionParticles[0].main.duration;

		foreach (ParticleSystem ps in explosionParticles)
			ps.Play();

		yield return new WaitForSeconds(partSysDuration);

		Collider myCollider = GetComponent<Collider>();
		if (myCollider != null)
			myCollider.enabled = false;

		yield return new WaitForSeconds(partSysDuration);
	}
}
