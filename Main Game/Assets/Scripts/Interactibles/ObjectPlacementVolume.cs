using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ObjectPlacementVolume : MonoBehaviour {
	public GameObject requiredObject;

	public abstract void PlacementTrigger(InteractiveObject gameObject);
}
