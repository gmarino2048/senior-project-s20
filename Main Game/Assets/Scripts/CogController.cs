using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogController : MonoBehaviour
{
	public static event Action Connected = delegate { };

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.tag);
		if(collision.gameObject.tag == "SnapCollider")
		{
			Connected();
		}
	}
}
