using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : ObjectPlacementVolume
{
	public GameObject toActivate;

	public override void PlacementTrigger(InteractiveObject placedObject)
	{
		Debug.Log("Unlocked = " + (placedObject,gameObject == requiredObject.gameObject));
		Debug.Log("Placed object: " + placedObject.gameObject);
		Debug.Log("Required object: " + requiredObject.gameObject);
		if (requiredObject.gameObject == placedObject.gameObject)
		{
			gameObject.SetActive(false);
			toActivate.SetActive(true);
		}
	}
}
