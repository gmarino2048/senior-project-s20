using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//Used for any object that the user can point at and interact with
[RequireComponent(typeof(Collider))]
public abstract class PointClickObject : MonoBehaviour {
	private Vector3 originalScale;
	[SerializeField] private float scaleAmount = 1.01f;

	protected virtual void Start() {
		originalScale = transform.localScale;
	}

	public virtual void Highlight() {
		StartCoroutine(HighlightCoroutine());
	}

	protected virtual IEnumerator HighlightCoroutine() {
		transform.localScale *= scaleAmount;
		yield return new WaitForEndOfFrame();
		transform.localScale = originalScale;
	}

	//Called when the player clicks on the object
	public abstract void Interact(HandController hand);

	//Called when the player releases the click
	public virtual void Release(HandController hand) { }
}