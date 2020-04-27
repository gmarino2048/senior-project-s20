using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBBobo : MonoBehaviour {
	[SerializeField] private float moveSpeed;
	[SerializeField] private float walkAnimSpeed;
	[SerializeField] private bool walkingLeft;
	[SerializeField] private float playerBounceVelocity;
	private float walkAnimTimer;
	private SpriteRenderer mySprite;
	private AudioSource bounceSound;
	[SerializeField] private Collider2D physicalCollider;

	void OnEnable() {
		mySprite = GetComponent<SpriteRenderer>();
		bounceSound = GetComponent<AudioSource>();
		//Reverses it since it flips around as soon as it touches the ground
		walkingLeft = !walkingLeft;
	}

	void Update() {
		if (walkingLeft)
			transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, 0));
		else
			transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
		RunWalkCycle();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		GBPlayerController player = collision.gameObject.GetComponent<GBPlayerController>();
		if (player != null)
			player.Kill();
		else
			walkingLeft = !walkingLeft;
		RunWalkCycle();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		GBPlayerController player = collision.gameObject.GetComponent<GBPlayerController>();
		if (player != null) {
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, playerBounceVelocity));
			physicalCollider.enabled = false;
			StartCoroutine(PlaySound());
		}
	}
	private IEnumerator PlaySound() {
		bounceSound.Play();
		yield return new WaitForSeconds(bounceSound.clip.length);
		Destroy(this.gameObject);
	}

	private void RunWalkCycle() {
		walkAnimTimer -= Time.deltaTime;
		if (walkAnimTimer < 0) {
			walkAnimTimer = walkAnimSpeed;
			mySprite.flipX = !mySprite.flipX;
		}
	}
}
