using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : ObjectPlacementVolume
{
	public GameObject toActivate;

	public override void PlacementTrigger(InteractiveObject placedObject)
	{
		if(requiredObject == placedObject)
		{
			gameObject.SetActive(false);
			toActivate.SetActive(true);
		}
	}
}
