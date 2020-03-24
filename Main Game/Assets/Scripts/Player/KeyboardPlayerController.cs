using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//Now this is the only type of playercontroller :(
public class KeyboardPlayerController : MonoBehaviour {
	public float speed;
	public float accel;
	public float raycastRange;

	private Rigidbody rb;

	private Transform camTF;
	private float camXRotation; //Used to keep the camera from flipping over vertically
	public float mouseSensitivity;
	private bool lockCam;

	private MagnifyingGlassManager magnifyingGlass;

	[SerializeField]
	private Transform leftHandTF, rightHandTF;
	private InteractiveObject leftHeldObject, rightHeldObject;
	//The transform in the center of the screen where held objects are placed
	public Transform objectDropPoint;

	void Start() {
		rb = GetComponent<Rigidbody>();
		camTF = Camera.main.transform;
		magnifyingGlass = GetComponentInChildren<MagnifyingGlassManager>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Only used for camera movement
	private void Update() {
		CamMovement();
		InteractiveObject target = CheckRaycast();
		HandHandler(target);

		//Bound to 'q' since it's convenient and shaped the most like a magnifying glass
		if (Input.GetKeyDown(KeyCode.Q)) {
			magnifyingGlass.Toggle();
		}
	}

	private void HandHandler(InteractiveObject target) {
		if (Input.GetMouseButtonDown(0)) {
			if (target != null && leftHeldObject == null) {
				target.Interact(leftHandTF);
				if (target.InteractionIsHeld)
					leftHeldObject = target;
			}
			else if(leftHeldObject != null) {
				leftHeldObject.Release();
				leftHeldObject = null;
			}
		}
		else if (Input.GetMouseButtonDown(1)) {
			if (target != null && rightHeldObject == null) {
				target.Interact(rightHandTF);
				if (target.InteractionIsHeld)
					rightHeldObject = target;
			}
			else if(rightHeldObject != null) {
				rightHeldObject.Release();
				rightHeldObject = null;
			}
		}
	}

	private void FixedUpdate() {
		WalkMovement();
	}

	private void CamMovement() {
		if (!lockCam) {
			transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));
			float mouseY = -Input.GetAxis("Mouse Y");
			if (camXRotation < 90 && camXRotation > -90) //camXRotation keeps track of how far it has rotated on the X axis
			{                                           //If it hits 90 or -90 it won't let you move any farther that way
				camXRotation += mouseY * mouseSensitivity;
				camTF.Rotate(new Vector3(mouseY * mouseSensitivity, 0, 0));
			}
			else if (camXRotation > 90 && mouseY < 0) {
				camXRotation += mouseY * mouseSensitivity;
				camTF.Rotate(new Vector3(mouseY * mouseSensitivity, 0, 0));
			}
			else if (camXRotation < -90 && mouseY > 0) {
				camXRotation += mouseY * mouseSensitivity;
				camTF.Rotate(new Vector3(mouseY * mouseSensitivity, 0, 0));
			}

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void WalkMovement() {
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;
		//Then push it towards that velocity
		Vector3 velocity = rb.velocity;
		Vector3 velocityChange = targetVelocity - velocity; //Gets how much we need to change
		velocityChange.x = Mathf.Clamp(velocityChange.x, -accel, accel);
		velocityChange.y = 0;
		velocityChange.z = Mathf.Clamp(velocityChange.z, -accel, accel); //Clamps it at normal acceleration
		rb.AddForce(velocityChange, ForceMode.VelocityChange);
	}

	//Raycasts forwards to find objects
	private InteractiveObject CheckRaycast() {
		RaycastHit hit;
		InteractiveObject target;
		if (Physics.Raycast(camTF.position, camTF.TransformDirection(Vector3.forward), out hit, raycastRange)) {
			target = hit.collider.gameObject.GetComponent<InteractiveObject>();
			if (target != null) {
				target.Highlight();
				return target;
			}
		}
		return null;
	}

	public void SetCamLockState(bool locked) {
		lockCam = locked;
		if (locked) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
