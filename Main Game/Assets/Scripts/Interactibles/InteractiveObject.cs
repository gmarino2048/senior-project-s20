using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for any object that the user interacts with
[RequireComponent(typeof(Collider))]
public abstract class InteractiveObject : MonoBehaviour {
	private Vector3 originalScale;
	[SerializeField] private float scaleAmount = 1.01f;

	//If this is true, then interaction behavior will be held over multiple frames. False for things where the player clicks & they run Interact once
	public bool InteractionIsHeld;

	public bool IsSpawner;
	public InteractiveObject ObjectToSpawn;
	private InteractiveObject SpawnedObject;

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
	public abstract void Interact(Transform handTF);

	//Called when the player releases the click
	public virtual void Release() { }

	public virtual void ReleaseTo(Transform placementPosition) {
		transform.parent = placementPosition;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	public virtual InteractiveObject GenerateSpawnedObject()
	{
		SpawnedObject = Instantiate(ObjectToSpawn);
		return SpawnedObject;
	}

	public virtual InteractiveObject GetSpawnedObject()
	{
		return SpawnedObject;
	}
}