using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PointClickButton : PointClickObject {
	[SerializeField] private float pressTime;
	[SerializeField] private float pressSpeed;
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
			transform.Translate(new Vector3(0, -pressSpeed * Time.deltaTime, 0));
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator ReleaseButton() {
		float timer = pressTime;
		releaseEvent.Invoke();

		while (timer > 0) {
			timer -= Time.deltaTime;
			transform.Translate(new Vector3(0, pressSpeed * Time.deltaTime, 0));
			yield return new WaitForEndOfFrame();
		}
		isPressing = false;
		transform.localPosition = originalPos;
	}
}
