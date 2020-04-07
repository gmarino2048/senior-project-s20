using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogPlacement : ObjectPlacementVolume
{
	public InteractiveObject requiredInteractiveObject;
	public static event Action<int, bool> CogPlaced = delegate { };
	public override void PlacementTrigger(InteractiveObject interactiveObject)
	{
		Debug.Log("Required interactive object: " + requiredInteractiveObject);
		Debug.Log("Placed interactive object: " + interactiveObject);
		Debug.Log("Correct object placed: " + (requiredInteractiveObject == interactiveObject));
		int slot = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
		CogPlaced(slot, (requiredInteractiveObject == interactiveObject));
	}
}
