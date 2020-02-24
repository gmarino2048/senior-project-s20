using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyboardPlayerController : MonoBehaviour {
	public float speed;
	public float accel;

	private Rigidbody rb;

	private Transform camTF;
	private float camXRotation; //Used to keep the camera from flipping over vertically
	public float mouseSensitivity;
	private bool lockCam;

	void Start() {
		rb = GetComponent<Rigidbody>();
		camTF = Camera.main.transform;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Only used for camera movement
	private void Update() {
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
	public void SetCamLockState(bool locked) {
		lockCam = locked;
		if (locked) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void FixedUpdate() {
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
}
