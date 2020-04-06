using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogObject : InteractiveObject
{
	//The player drops held items from this point
	private Transform playerCenterScreen;
	protected override void Start()
	{
		base.Start();
		InteractionIsHeld = true;
		playerCenterScreen = FindObjectOfType<KeyboardPlayerController>().objectDropPoint;
	}

	public override void Interact(Transform handTF)
	{
		transform.parent = handTF;
		transform.localPosition = new Vector3(0, 0, 0);
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Collider>().enabled = false;
	}

	public override void Release()
	{
		transform.parent = null;
		transform.position = playerCenterScreen.position;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Collider>().enabled = true;
	}

	public override void ReleaseTo(Transform placementPosition)
	{
		transform.parent = placementPosition;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		GetComponent<Collider>().enabled = true;
	}
}
