using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogController : MonoBehaviour
{
	[SerializeField] private int cogID;

	public static event Action<string, GameObject> Connected = delegate { };

	private void OnCollisionEnter(Collision collision)
	{
		/*if(collision.gameObject.tag == "SnapCollider")
		{
			Connected();
		}*/
		/*Debug.Log(collision.gameObject.GetComponentInParent<BoxCollider>() != null);
		Debug.Log(collision.gameObject.GetComponentInParent<SnapController>() != null);*/	
		if (collision.gameObject.GetComponent<BoxCollider>() != null && collision.gameObject.GetComponent<SnapController>() != null)
		{
			Connected(name, this.gameObject);
		}
	}
}
