using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : PotionInteractionObject {
	[SerializeField] ParticleSystem[] explosionParticles;
	private AudioSource crumblingSound;

	void Start() {
		requiredEffect = PotionType.Explosive;
		crumblingSound = GetComponent<AudioSource>();
	}

	public override IEnumerator PotionEffects()
	{
		GetComponent<Collider>().enabled = false;
		float partSysDuration = explosionParticles[0].main.duration;
		crumblingSound.Play();

		foreach (ParticleSystem ps in explosionParticles)
			ps.Play();

		yield return new WaitForSeconds(partSysDuration);

		Collider myCollider = GetComponent<Collider>();
		if (myCollider != null)
			myCollider.enabled = false;

		yield return new WaitForSeconds(partSysDuration);
		this.gameObject.SetActive(false);
	}
}
