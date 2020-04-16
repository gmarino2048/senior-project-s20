using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : PotionInteractionObject {
	[SerializeField] private PlantSegment[] plantSegments;
	[SerializeField] private GameObject plantColliders, noPlantColliders;

	[SerializeField] private float iterationSpeed;

	private bool isPlayingEffect;

	public override void HitByPotion() {
		if (!isPlayingEffect) {
			isPlayingEffect = true;
			if (requiredEffect == PotionType.Plant) {
				StartCoroutine(PotionEffects(true));
				requiredEffect = PotionType.Fire;
			}
			else if (requiredEffect == PotionType.Fire) {
				StartCoroutine(PotionEffects(false));
				requiredEffect = PotionType.Plant;
			}
		}
	}

	public override IEnumerator PotionEffects() {
		throw new System.NotImplementedException();
	}

	private IEnumerator PotionEffects(bool growPlants) {
		plantColliders.SetActive(growPlants);
		noPlantColliders.SetActive(!growPlants);

		foreach (PlantSegment plant in plantSegments) {
			if (growPlants)
				plant.gameObject.SetActive(true);
			else
				StartCoroutine(plant.PotionEffects(false));
			yield return new WaitForSeconds(iterationSpeed);
		}

		isPlayingEffect = false;
	}
}
