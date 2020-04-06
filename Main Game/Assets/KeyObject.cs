using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyObject : InteractiveObject
{
	[SerializeField]
	private GameObject lockedObject;

	public static event Action<bool, GameObject> Unlocked = delegate { };
	protected override void Start()
	{
		base.Start();
		InteractionIsHeld = false;
	}

	public override void Interact(Transform handTF)
	{
		Debug.Log("Grabbed Key");
		Unlocked(true, lockedObject);
		gameObject.SetActive(false);
	}

	public override void Release()
	{

	}
}
