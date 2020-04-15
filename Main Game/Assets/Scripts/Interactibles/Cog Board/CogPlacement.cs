using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogPlacement : ObjectPlacementVolume
{
	public int slot;
	public InteractiveObject requiredInteractiveObject;
	public static event Action<int, bool> CogPlaced = delegate { };
	public override void PlacementTrigger(InteractiveObject interactiveObject)
	{
		//Debug.Log("Required interactive object: " + requiredInteractiveObject);
		//Debug.Log("Placed interactive object: " + interactiveObject);
		//Debug.Log("Correct object placed: " + (requiredInteractiveObject == interactiveObject));

		//The new tag check for the placements should let up put wrong cogs in
		CogPlaced(slot, (requiredInteractiveObject == interactiveObject));
	}
}
