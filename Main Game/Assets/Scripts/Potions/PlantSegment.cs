using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSegment : PotionInteractionObject {
	private PlantManager parentManager;
	[SerializeField] private ParticleSystem growthParticles, burnParticles;
	[SerializeField] private AudioSource growthSound, burnSound;

	void Start() {
		parentManager = GetComponentInParent<PlantManager>();
		requiredEffect = PotionType.Fire;
	}

	private void Awake() {
		StartCoroutine(PotionEffects(true));
	}

	public override IEnumerator PotionEffects() {
		throw new System.NotImplementedException();
	}

	//When any plant segment gets hit by fire, it tells its parent PlantManager, which triggers all of them to burn
	public override void HitByPotion() {
		parentManager.HitByPotion();
	}

	public IEnumerator PotionEffects(bool isGrowing) {
		ParticleSystem activePS = isGrowing ? growthParticles : burnParticles;
		if (isGrowing)
			growthSound.Play();
		else
			burnSound.Play();

		activePS.Play();
		yield return new WaitForSeconds(activePS.main.duration);
		GetComponent<MeshRenderer>().enabled = isGrowing;
		GetComponent<Collider>().enabled = isGrowing;
		yield return new WaitForSeconds(activePS.main.duration);
		gameObject.SetActive(isGrowing);
	}
}
