using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PointClickButton : PointClickObject
{
	[SerializeField] private float pressTime;
	[SerializeField] private float pressSpeed;
	[SerializeField] private bool moveOnX, moveOnY, moveOnZ;
	private int xMove, yMove, zMove;

	private bool isPressing;
	private Vector3 originalPos;

	[Serializable]
	public class ButtonClickedEvent : UnityEvent { }
	//Event delegates triggered on click
	[SerializeField] private ButtonClickedEvent pressEvent = new ButtonClickedEvent();
	[SerializeField] private ButtonClickedEvent releaseEvent = new ButtonClickedEvent();

	protected override void Start() {
		base.Start();
		originalPos = transform.localPosition;

		if (moveOnX)
			xMove = 1;
		if (moveOnY)
			yMove = 1;
		if (moveOnZ)
			zMove = 1;
	}

	public override void Interact(HandController hand) {
		if (!isPressing)
			StartCoroutine(PressButton());
	}

	public override void Release(HandController hand) {
		if (isPressing)
			StartCoroutine(ReleaseButton());
	}

	private IEnumerator PressButton() {
		float timer = 0;
		pressEvent.Invoke();
		isPressing = true;

		while (timer < pressTime) {
			timer += Time.deltaTime;
			float distance = -pressSpeed * Time.deltaTime;
			transform.Translate(new Vector3(distance * xMove, distance * yMove, distance * zMove));
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator ReleaseButton() {
		float timer = pressTime;
		releaseEvent.Invoke();

		while (timer > 0) {
			timer -= Time.deltaTime;
			float distance = pressSpeed * Time.deltaTime;
			transform.Translate(new Vector3(distance * xMove, distance * yMove, distance * zMove));
			yield return new WaitForEndOfFrame();
		}
		isPressing = false;
		transform.localPosition = originalPos;
	}
}
