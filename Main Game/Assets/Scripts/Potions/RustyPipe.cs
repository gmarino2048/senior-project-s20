using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RustyPipe : PotionInteractionObject {
	[SerializeField] private GameObject waterPlane;
	[SerializeField] private GameObject objectToDestroy;
	[SerializeField] private float raiseHeight, raiseDuration;
	private float originalWaterHeight;
	private ParticleSystem particles;
	private AudioSource burstSound;

	private void Start() {
		particles = GetComponentInChildren<ParticleSystem>();
		originalWaterHeight = waterPlane.transform.position.y;
		burstSound = GetComponent<AudioSource>();
	}

	public override IEnumerator PotionEffects() {
		//Do animation
		yield return new WaitForSeconds(0.5f);
		particles.Play();
		burstSound.Play();
		yield return new WaitForSeconds(1);

		float timer = 0;
		Vector3 newWaterPos;
		if (objectToDestroy != null)
			Destroy(objectToDestroy);
		while(timer < raiseDuration) {
			timer += Time.deltaTime;
			newWaterPos = waterPlane.transform.position;
			newWaterPos.y = Mathf.Lerp(originalWaterHeight, originalWaterHeight + raiseHeight, timer / raiseDuration);
			waterPlane.transform.position = newWaterPos;
			yield return new WaitForEndOfFrame();
		}
		particles.Stop();
	}
}
