using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyObject : HoldableObject
{
	[SerializeField]
	private GameObject lockedObject;

	public static event Action<bool, GameObject> Unlocked = delegate { };
	protected override void Start()
	{
		base.Start();
	}
	public override void ReleaseTo(Transform placementPosition)
	{
		transform.parent = placementPosition;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		GetComponent<Collider>().enabled = false;
	}
}
