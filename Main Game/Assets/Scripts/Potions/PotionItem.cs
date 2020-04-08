using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : HoldableObject {
	[SerializeField] protected PotionType effectType;
	[SerializeField] private Vector3 throwVector;
	[SerializeField] private float throwTorque;
	[SerializeField] protected float splashRadius;

	protected ParticleSystem particles;
	protected Rigidbody rb;

	protected bool hasBeenThrown;

	protected override void Start() {
		base.Start();
		particles = GetComponentInChildren<ParticleSystem>();
		rb = GetComponent<Rigidbody>();
	}

	public override void Release() {
		base.Release();
		hasBeenThrown = true;
		rb.AddRelativeForce(throwVector);
		//Tried very hard to make it spin faster but increasing torque any amount beyond ~6 doesn't seem to do anything
		rb.AddRelativeTorque(new Vector3(throwTorque, 0), ForceMode.VelocityChange);
	}

	protected virtual void OnCollisionEnter(Collision other) {
		if (hasBeenThrown) {
			StartCoroutine(Shatter());
		}
	}

	protected virtual IEnumerator Shatter() {
		particles.Play();

		Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, splashRadius);
		foreach(Collider collider in collidersInRadius) {
			PotionInteractionObject colliderPotionInteraction = collider.GetComponent<PotionInteractionObject>();
			if (colliderPotionInteraction != null) {
				if(colliderPotionInteraction.requiredEffect == effectType) {
					StartCoroutine(colliderPotionInteraction.HitByPotion());
				}
			}
		}

		yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
		Destroy(this.gameObject);
	}
}
