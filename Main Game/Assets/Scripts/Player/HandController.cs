using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour {
	[SerializeField] private float raycastRange;
	//Only good for the testing rig - in the real game things will hover in the player's hand
	[SerializeField] private Transform hoverPosition;

	private bool handFree = true;
	private GameObject heldObject;

	[SerializeField] private SteamVR_Input_Sources hand;
	private VRInputManager vrIn;

	private void Start() {
		vrIn = FindObjectOfType<VRInputManager>();
	}

	void Update() {
		if (handFree) {
			GameObject targetObject = CheckRaycast();
			if (targetObject != null) {
				PointClickObject targetPCO = targetObject.GetComponent<PointClickObject>();
				if (targetPCO != null) {
					targetPCO.Highlight();
					if (vrIn.GetTrigger(hand)) {
						heldObject = targetObject;
						handFree = false;
						targetPCO.Interact(this);
					}
				}
				else if (vrIn.GetTrigger(hand)) {
					heldObject = targetObject;
					handFree = false;
					targetObject.transform.parent = hoverPosition;
					targetObject.transform.localPosition = new Vector3(0, 0, 0);
					targetObject.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
		//Do things to the held item
		else {
			if (!vrIn.GetTrigger(hand)) {
				if (!handFree) {
					PointClickObject targetPCO = heldObject.GetComponent<PointClickObject>();
					if (targetPCO != null)
						targetPCO.Release(this);
					else {
						heldObject.GetComponent<Rigidbody>().isKinematic = false;
						heldObject.transform.parent = null;
					}
					heldObject = null;
					handFree = true;
				}
			}
		}
	}

	//Raycasts forwards to find objects. Assigns hit object to heldItem and highlights it
	//This probably should be replaced for the real VR controllers
	private GameObject CheckRaycast() {
		RaycastHit hit;
		GameObject target;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange)) {
			target = hit.collider.gameObject;
			if (target.tag == "Interactive") {
				return target;
			}
		}
		return null;
	}

	public Transform GetHoverTF() {
		return hoverPosition;
	}
}
