using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDemoCube : MonoBehaviour {
	public float moveSpeed;
	public Vector3 jumpForce;

	private Rigidbody rb;
	private bool grounded;

	private bool isActive = false;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		Vector3 position = transform.position;
		float newX = position.x;

		if (isActive) {
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				newX -= moveSpeed;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				newX += moveSpeed;
			}
			transform.position = new Vector3(newX, position.y, position.z);

			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (grounded)
					rb.AddForce(jumpForce);
			}
		}
	}

	private void OnCollisionEnter(Collision collision) {
		grounded = true;
	}

	private void OnCollisionExit(Collision collision) {
		grounded = false;
	}

	public void SetControlState(bool cubeIsActive) {
		Debug.Log("set to " + cubeIsActive);
		isActive = cubeIsActive;
	}
}
