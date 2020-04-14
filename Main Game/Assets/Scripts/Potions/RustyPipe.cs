using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyPipe : PotionInteractionObject {
	[SerializeField] private GameObject waterPlane;
	[SerializeField] private float raiseHeight, raiseDuration;
	private float originalWaterHeight;
	private ParticleSystem particles;

	private void Start() {
		particles = GetComponentInChildren<ParticleSystem>();
		originalWaterHeight = waterPlane.transform.position.y;
	}

	public override IEnumerator PotionEffects() {
		//Do animation
		yield return new WaitForSeconds(0.5f);
		particles.Play();
		yield return new WaitForSeconds(1);

		float timer = 0;
		Vector3 newWaterPos;
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
