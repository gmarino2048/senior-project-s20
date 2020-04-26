using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogObject : HoldableObject
{
	public override void ReleaseTo(Transform placementPosition)
	{
		transform.parent = placementPosition;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		GetComponent<Collider>().enabled = false;
	}
}
