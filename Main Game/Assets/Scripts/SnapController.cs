﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnapController: MonoBehaviour
{
	[SerializeField] private GameObject[] permittedSnappables;
	private void OnTriggerStay(Collider other)
	{
		if(Array.Exists(permittedSnappables, snappable => snappable == other.gameObject))
		{

			// Using switch in case we want to add more than one collider for an object
			switch (this.transform.tag) 
			{
				case "TopSnapCollider":
					other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.   position.z); // Used to lock the snappable object with respect to the snappable collider of the original object
					other.transform.rotation = transform.rotation;
					other.GetComponentInParent<Rigidbody>().useGravity = false;
					break;
			}
		}
	}
}
