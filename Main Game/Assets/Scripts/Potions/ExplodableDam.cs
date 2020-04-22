using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableDam : PotionInteractionObject {
	[SerializeField] ParticleSystem[] explosionParticles;
	[SerializeField] private GameObject waterObject;
	[SerializeField] private GameObject explodableObject;
	[SerializeField] private float raiseDuration;
	[SerializeField] private float raiseZ;
	private float originalWaterZ;

	void Start() {
		requiredEffect = PotionType.Explosive;
		originalWaterZ = waterObject.transform.position.z;
	}

	public override IEnumerator PotionEffects() {
		explodableObject.SetActive(false);
		float partSysDuration = explosionParticles[0].main.duration;

		foreach (ParticleSystem ps in explosionParticles)
			ps.Play();

		Collider myCollider = explodableObject.GetComponent<Collider>();
		if (myCollider != null)
			myCollider.enabled = false;

		float timer = 0;
		Vector3 newWaterPos;
		while(timer < raiseDuration) {
			timer += Time.deltaTime;
			newWaterPos = waterObject.transform.position;
			newWaterPos.z = Mathf.Lerp(originalWaterZ, originalWaterZ + raiseZ, timer / raiseDuration);
			waterObject.transform.position = newWaterPos;
			yield return new WaitForEndOfFrame();
		}

		
	}
}
