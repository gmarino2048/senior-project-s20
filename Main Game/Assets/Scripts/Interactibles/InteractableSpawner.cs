using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawner : InteractiveObject
{
	public override void Interact(Transform handTF)
	{
		//Debug.Log(GetSpawnedObject());
		GetSpawnedObject().transform.parent = handTF;
		GetSpawnedObject().transform.localPosition = Vector3.zero;
		GetSpawnedObject().transform.localRotation = Quaternion.identity;
		GetSpawnedObject().GetComponent<Rigidbody>().isKinematic = true;
		GetSpawnedObject().GetComponent<Collider>().enabled = false;
	}
}
