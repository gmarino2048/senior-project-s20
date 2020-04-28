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

	public void TriggerFromWater(Transform waterTF) {
		if (hasBeenThrown) {
			hasBeenThrown = false;
			StartCoroutine(Shatter());
			Vector3 icePos = transform.position;
			icePos.y = waterTF.position.y;
			GameObject ice = Instantiate(iceberg, icePos, Quaternion.identity);
			ice.transform.parent = waterTF;
		}
	}

	protected override IEnumerator Shatter() {
		glassParticles.Play();
		splashParticles.Play();
		glassBreak.Play();
		effectSound.Play();
		rb.isKinematic = true;
		glowLight.intensity = brokenLightIntensity;
		GetComponent<MeshRenderer>().enabled = false;

		yield return new WaitForSeconds(splashParticles.main.duration * 0.75f);

		float timer = 0;
		float goalTime = splashParticles.main.duration * 0.25f;
		while (timer < goalTime) {
			timer += Time.deltaTime;
			glowLight.intensity = Mathf.Lerp(brokenLightIntensity, 0, timer / goalTime);
			yield return new WaitForEndOfFrame();
		}
		Destroy(this.gameObject);
	}
}
