using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : HoldableObject {
	[SerializeField] private PotionType effectType;
	[SerializeField] private Vector3 throwVector;
	[SerializeField] private float splashRadius;

	private ParticleSystem particles;
	private Rigidbody rb;

	private bool hasBeenThrown;

	protected override void Start() {
		base.Start();
		particles = GetComponentInChildren<ParticleSystem>();
		rb = GetComponent<Rigidbody>();
	}

	public override void Release() {
		base.Release();
		hasBeenThrown = true;
		rb.AddRelativeForce(throwVector);
	}

	void OnCollisionEnter(Collision other) {
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
					Debug.Log("Hit " + collider.gameObject.name);
					StartCoroutine(colliderPotionInteraction.HitByPotion());
				}
			}
		}

		yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
		Destroy(this.gameObject);
	}
}
