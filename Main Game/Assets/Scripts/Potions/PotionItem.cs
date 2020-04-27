using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : HoldableObject {
	[Header("Potion Settings")]
	[SerializeField] protected PotionType effectType;
	[SerializeField] private Vector3 throwVector;
	[SerializeField] private float throwTorque;
	[SerializeField] protected float splashRadius;
	[SerializeField] protected ParticleSystem splashParticles, glassParticles;
	protected Light glowLight;
	[SerializeField] protected float brokenLightIntensity;
	[SerializeField] protected AudioSource glassBreak, effectSound;

	protected Rigidbody rb;

	protected bool hasBeenThrown;

	protected override void Start() {
		base.Start();
		rb = GetComponent<Rigidbody>();
		glowLight = GetComponentInChildren<Light>();
	}

	public override void Release() {
		base.Release();
		hasBeenThrown = true;
		rb.AddRelativeForce(throwVector);
		rb.interpolation = RigidbodyInterpolation.Interpolate;
		//Tried very hard to make it spin faster but increasing torque any amount beyond ~6 doesn't seem to do anything
		rb.AddRelativeTorque(new Vector3(throwTorque, 0), ForceMode.VelocityChange);
	}

	protected virtual void OnCollisionEnter(Collision other) {
		if (hasBeenThrown) {
			StartCoroutine(Shatter());
			hasBeenThrown = false;
		}
	}

	protected virtual IEnumerator Shatter() {
		glassParticles.Play();
		splashParticles.Play();
		glassBreak.Play();
		effectSound.Play();
		Debug.Log("Shattering");
		rb.isKinematic = true;
		glowLight.intensity = brokenLightIntensity;
		GetComponent<MeshRenderer>().enabled = false;

		Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, splashRadius);
		foreach(Collider collider in collidersInRadius) {
			PotionInteractionObject colliderPotionInteraction = collider.GetComponent<PotionInteractionObject>();
			if (colliderPotionInteraction != null) {
				if(colliderPotionInteraction.requiredEffect == effectType) {
					colliderPotionInteraction.HitByPotion();
				}
			}
		}

		yield return new WaitForSeconds(splashParticles.main.duration * 0.75f);
		float timer = 0;
		float goalTime = splashParticles.main.duration * 0.25f;
		while(timer < goalTime) {
			timer += Time.deltaTime;
			glowLight.intensity = Mathf.Lerp(brokenLightIntensity, 0, timer / goalTime);
			yield return new WaitForEndOfFrame();
		}
		Destroy(this.gameObject);
	}
}
