using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBPlayerController : MonoBehaviour {
	public float moveSpeed;

	//For jumping:
	public Vector3 jumpForce;
	[SerializeField] private Transform leftFoot, rightFoot;
	private float coyoteTimer;

	private Rigidbody rb;

	private bool isActive = false;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		Vector3 position = transform.position;
		float newX = position.x;
		bool canJump = updateGrounded();

		if (isActive) {
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				newX -= moveSpeed;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				newX += moveSpeed;
			}
			transform.position = new Vector3(newX, position.y, position.z);

			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (canJump) {
					coyoteTimer = 0;
					rb.AddForce(jumpForce);
				}
			}
		}
	}

	private bool updateGrounded() {
		if (Physics.Raycast(leftFoot.position, -transform.up, 0.01f) || Physics.Raycast(rightFoot.position, -transform.up, 0.01f)) {
			return true;
		}
		//Gives a bit of leeway when running off platforms 
		coyoteTimer -= Time.deltaTime;
		if (coyoteTimer > 0)
			return true;

		return false;
	}

	public void SetControlState(bool cubeIsActive) {
		isActive = cubeIsActive;
	}
}
