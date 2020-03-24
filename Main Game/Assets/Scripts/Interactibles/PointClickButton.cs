using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PointClickButton : InteractiveObject
{
	[SerializeField] private float pressTime;
	[SerializeField] private float pressSpeed;
	//How often the event fires while the button is being held down. If <= 0, it doesn't repeat
	[SerializeField] private float eventTriggerRate;
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

	public override void Interact(Transform handTF) {
		if (!isPressing)
			StartCoroutine(PressButton());
	}

	public override void Release() {
		if (isPressing) {
			StopAllCoroutines();
			StartCoroutine(ReleaseButton());
		}
	}

	private IEnumerator PressButton() {
		float timer = 0;
		isPressing = true;

		while (timer < pressTime) {
			timer += Time.deltaTime;
			float distance = -pressSpeed * Time.deltaTime;
			transform.Translate(new Vector3(distance * xMove, distance * yMove, distance * zMove));
			yield return new WaitForEndOfFrame();
		}

		pressEvent.Invoke();

		if (eventTriggerRate > 0) {
			while (true) {
				pressEvent.Invoke();
				yield return new WaitForSeconds(eventTriggerRate);
			}
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
