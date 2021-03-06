﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
	private FadeController fadeController;

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

	private float camStandHeight;
	[SerializeField] private float crouchDistance;
	[SerializeField] private float crouchDuration;
	private Coroutine crouchingCoroutine;
	private float standingSpeed, crouchingSpeed;

	void Start() {
		rb = GetComponent<Rigidbody>();
		camTF = Camera.main.transform;
		camStandHeight = camTF.localPosition.y;
		standingSpeed = speed;
		crouchingSpeed = speed * 0.5f;
		magnifyingGlass = GetComponentInChildren<MagnifyingGlassManager>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		fadeController = FindObjectOfType<FadeController>();
		fadeController.SetTransparent();
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

			if (Input.GetKeyDown(KeyCode.LeftControl)) {
				if(crouchingCoroutine != null)
					StopCoroutine(crouchingCoroutine);
				speed = crouchingSpeed;
				crouchingCoroutine = StartCoroutine(Crouch(camStandHeight - crouchDistance));
			}
			else if (Input.GetKeyUp(KeyCode.LeftControl)) {
				if (crouchingCoroutine != null)
					StopCoroutine(crouchingCoroutine);
				speed = standingSpeed;
				crouchingCoroutine = StartCoroutine(Crouch(camStandHeight));
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
		SceneTransitionZone targetTransitionZone = null;
		if(target != null) {
			targetInteractive = target.GetComponent<InteractiveObject>();
			targetPlacement = target.GetComponent<ObjectPlacementVolume>();
			targetTransitionZone = target.GetComponent<SceneTransitionZone>();
			if (targetInteractive != null && targetInteractive.enabled)
				targetInteractive.Highlight();
		}

		//Click behaviors
		if (!magnifyingGlass.isEnabled) {
			if (Input.GetMouseButtonDown(0)) {
				if (targetTransitionZone != null && targetTransitionZone.notNeedMagGlass){
					transform.position = targetTransitionZone.target.spawnPoint;
					return;
				}

				if (targetInteractive != null && heldObject == null) {
					if (targetInteractive.IsSpawner) {
						heldObject = targetInteractive.GenerateSpawnedObject();
					}
					else if (targetInteractive.InteractionIsHeld)
						heldObject = targetInteractive;
					targetInteractive.Interact(handTF);
				}
				else if (heldObject != null) {
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
	}

	private void CamMovement() {
		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));
		float mouseY = -Input.GetAxis("Mouse Y");
		if (camXRotation < 90 && camXRotation > -90) //camXRotation keeps track of how far it has rotated on the X axis
		{                                           //If it hits 90 or -90 it won't let you move any farther that way
			camXRotation += mouseY * mouseSensitivity;
			camTF.Rotate(new Vector3(mouseY * mouseSensitivity, 0, 0));
		}
		else if (camXRotation >= 90 && mouseY < 0) {
			camXRotation += mouseY * mouseSensitivity;
			camTF.Rotate(new Vector3(mouseY * mouseSensitivity, 0, 0));
		}
		else if (camXRotation <= -90 && mouseY > 0) {
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

	public IEnumerator OutOfBounds(Transform spawnPoint, float fadeInTime, float fadeOutTime) {
		StartCoroutine(ScreenFade(fadeInTime, fadeOutTime, Color.black));
		yield return new WaitForSeconds(fadeInTime);
		transform.position = spawnPoint.position;
	}

	public IEnumerator ScreenFade(float fadeOutTime, float fadeInTime, Color screenColor)
	{
		yield return fadeController.FadeOut(screenColor, fadeOutTime);
		yield return new WaitForSeconds(0.5f);
		yield return fadeController.FadeIn(fadeInTime);
	}

	private IEnumerator Crouch(float goalHeight) {
		float newHeight;
		float startHeight = camTF.localPosition.y;
		float timer = 0;

		while (timer < crouchDuration) {
			newHeight = Mathf.Lerp(startHeight, goalHeight, timer / crouchDuration);
			camTF.localPosition = new Vector3(0, newHeight, 0);
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	public static void ActivateMagnifyingGlass()
	{
		hasMagnifyingGlass = true;
	}
}
