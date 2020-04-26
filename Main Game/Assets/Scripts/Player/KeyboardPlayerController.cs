using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//Now this is the only type of playercontroller :(
public class KeyboardPlayerController : MonoBehaviour {
	public float speed;
	public float accel;
	public float raycastRange;

	private Transform camTF;
	private float camXRotation; //Used to keep the camera from flipping over vertically
	public float mouseSensitivity;
	[SerializeField] private GameObject reticle;

	private Rigidbody rb;
	private MagnifyingGlassManager magnifyingGlass;

	[SerializeField]
	private Transform handTF;
	private InteractiveObject heldObject;
	//The transform in the center of the screen where held objects are placed
	public Transform objectDropPoint;

	private bool movementLocked = false;
	[SerializeField]
	private static bool hasMagnifyingGlass = false;

	void Start() {
		rb = GetComponent<Rigidbody>();
		camTF = Camera.main.transform;
		magnifyingGlass = GetComponentInChildren<MagnifyingGlassManager>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Only used for camera movement
	private void Update() {
		if (!movementLocked) {
			CamMovement();
			GameObject target = CheckRaycast();
			HandHandler(target);

			//Bound to 'q' since it's convenient and shaped the most like a magnifying glass
			if (hasMagnifyingGlass && Input.GetKeyDown(KeyCode.Q)) {
				magnifyingGlass.Toggle();
			}
		}
	}

	private void FixedUpdate() {
		if (!movementLocked) {
			WalkMovement();
		}
	}

	private void HandHandler(GameObject target) {
		//First get the possible interactable components out
		InteractiveObject targetInteractive = null;
		ObjectPlacementVolume targetPlacement = null;
		if(target != null) {
			targetInteractive = target.GetComponent<InteractiveObject>();
			targetPlacement = target.GetComponent<ObjectPlacementVolume>();
			if (targetInteractive != null)
				targetInteractive.Highlight();
		}

		//Click behaviors
		if (Input.GetMouseButtonDown(0)) {
			if (targetInteractive != null && heldObject == null) {
				if (targetInteractive.IsSpawner)
				{
					//Debug.Log("IsSpawner");
					heldObject = targetInteractive.GenerateSpawnedObject();
				}
				else if (targetInteractive.InteractionIsHeld)
					heldObject = targetInteractive;
				//Debug.Log(heldObject.name);
				targetInteractive.Interact(handTF);
			}
			else if(heldObject != null) {
				if (targetPlacement != null) {
					if (targetPlacement.requiredObject == heldObject.gameObject || heldObject.gameObject.tag == targetPlacement.requiredTag) {
						heldObject.ReleaseTo(targetPlacement.transform);
						targetPlacement.PlacementTrigger(heldObject);
						heldObject = null;
						return;
					}
				}
				heldObject.Release();
				heldObject = null;
			}
		}
	}

	private void CamMovement() {
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
	private GameObject CheckRaycast() {
		RaycastHit hit;
		GameObject target;
		if (Physics.Raycast(camTF.position, camTF.TransformDirection(Vector3.forward), out hit, raycastRange)) {
			target = hit.collider.gameObject;
			if (target != null) {
				return target;
			}
		}
		return null;
	}

	public void SetPlayerMovementLock(bool lockPlayer) {
		movementLocked = lockPlayer;
		Cursor.visible = lockPlayer;
		if (lockPlayer) {
			rb.velocity = Vector3.zero;
			Cursor.lockState = CursorLockMode.None;
			reticle.SetActive(false);
		}
		else {
			Cursor.lockState = CursorLockMode.Locked;
			reticle.SetActive(true);
		}
	}
	
	public void SetCameraState(bool enablePlayerCamera) {
		camTF.GetComponent<Camera>().enabled = enablePlayerCamera;
		camTF.GetComponent<AudioListener>().enabled = enablePlayerCamera;
	}

	public static void ActivateMagnifyingGlass()
	{
		hasMagnifyingGlass = true;
	}
}
