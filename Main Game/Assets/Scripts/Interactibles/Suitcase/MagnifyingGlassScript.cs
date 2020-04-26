using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassScript : InteractiveObject
{
	public override void Interact(Transform handTF)
	{
		KeyboardPlayerController.ActivateMagnifyingGlass();
		gameObject.SetActive(false);
		Debug.Log("Interacted w/ magnifying glass.");
	}
}
