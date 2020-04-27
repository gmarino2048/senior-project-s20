using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBPlayerController : MonoBehaviour {
	public float moveSpeed;
	//For jumping:
	public Vector3 jumpForce;
	[SerializeField] private Transform leftFoot, rightFoot;
	private float coyoteTimer;
	private float jumpTimer;

	private Rigidbody2D rb;
	[SerializeField] private GBEdgeScroller scroller;

	private Vector2 spawnPos;

	[Header("Sprites")]
	[SerializeField] private Sprite idle;
	[SerializeField] private Sprite walk1, walk2, jump;
	private SpriteRenderer playerSprite;
	private bool onSprite1 = true;
	[SerializeField] private float walkAnimSpeed;
	private float walkAnimTimer;

	[Header("SFX")]
	[SerializeField] private AudioSource jumpSound;
	[SerializeField] private AudioSource hurtSound, coinSound;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		playerSprite = GetComponent<SpriteRenderer>();
		spawnPos = transform.position;
	}

	private void FixedUpdate() {
		Vector3 position = transform.position;

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, 0));
			playerSprite.flipX = true;
			RunWalkCycle();
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
			playerSprite.flipX = false;
			RunWalkCycle();
		}
		else {
			playerSprite.sprite = idle;
		}

		
	}

	//Only for jumping
	private void Update() {
		if (jumpTimer < 0) {
			bool canJump = updateGrounded();

			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (canJump) {
					jumpTimer = 0.1f;
					coyoteTimer = 0;
					rb.AddForce(jumpForce);
					jumpSound.Play();
				}
			}
			if (!canJump)
				playerSprite.sprite = jump;
		}
		else
			jumpTimer -= Time.deltaTime;
	}

	private bool updateGrounded() {
		if ((Physics2D.Raycast(leftFoot.position, -transform.up, 0.05f).collider != null) || (Physics2D.Raycast(rightFoot.position, -transform.up, 0.05f).collider != null)) {
			return true;
		}
		//Gives a bit of leeway when running off platforms 
		coyoteTimer -= Time.deltaTime;
		if (coyoteTimer > 0)
			return true;

		return false;
	}

	private void RunWalkCycle() {
		walkAnimTimer -= Time.deltaTime;
		if (walkAnimTimer < 0) {
			walkAnimTimer = walkAnimSpeed;
			if (onSprite1)
				playerSprite.sprite = walk2;
			else
				playerSprite.sprite = walk1;
			onSprite1 = !onSprite1;
		}
	}

	public void Kill() {
		scroller.Respawn();
		transform.position = spawnPos;
		hurtSound.Play();
	}
	public void GetCoin() {
		coinSound.Play();
	}
}
