using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
	[SerializeField] private GameObject key;

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject == key && !other.GetComponentInParent<DragObject>().IsHeld())
		{
			Unlock();
		}
	}

	private void Unlock()
	{
		Debug.Log(gameObject.name + " has been unlocked.");
	}
}
