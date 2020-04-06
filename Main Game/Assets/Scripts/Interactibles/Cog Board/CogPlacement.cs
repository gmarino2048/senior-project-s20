using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogPlacement : ObjectPlacementVolume
{
	public InteractiveObject requiredInteractiveObject;
	public static event Action<string, bool> CogPlaced = delegate { };
	public override void PlacementTrigger(InteractiveObject interactiveObject)
	{
		Debug.Log("Required interactive object: " + requiredInteractiveObject);
		Debug.Log("Placed interactive object: " + interactiveObject);
		Debug.Log("Correct object placed: " + (requiredInteractiveObject == interactiveObject));
		CogPlaced(gameObject.name, (requiredInteractiveObject == interactiveObject));
	}
}
