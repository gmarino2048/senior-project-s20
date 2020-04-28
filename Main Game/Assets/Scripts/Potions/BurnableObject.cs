using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : PotionInteractionObject {
	[SerializeField] GameObject[] burnableChildren;
	[SerializeField] ParticleSystem[] flameParticles;
	private AudioSource burnSound;

	void Start() {
		requiredEffect = PotionType.Fire;
		burnSound = GetComponent<AudioSource>();
	}

	public override IEnumerator PotionEffects() {
		float partSysDuration = flameParticles[0].main.duration;
		burnSound.Play();

		foreach (ParticleSystem ps in flameParticles)
			ps.Play();

		yield return new WaitForSeconds(partSysDuration);

		Collider myCollider = GetComponent<Collider>();
		if (myCollider != null)
			myCollider.enabled = false;
		foreach (GameObject burnObject in burnableChildren) {
			burnObject.GetComponent<MeshRenderer>().enabled = false;
			Collider objectsCollider = burnObject.GetComponent<Collider>();
			if(objectsCollider != null)
				objectsCollider.enabled = false;
		}

		yield return new WaitForSeconds(partSysDuration);

		foreach (GameObject burnObject in burnableChildren)
			burnObject.SetActive(false);

		this.gameObject.SetActive(false);
	}
}
