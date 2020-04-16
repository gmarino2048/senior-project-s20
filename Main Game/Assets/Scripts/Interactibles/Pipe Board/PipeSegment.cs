using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSegment : MonoBehaviour {
	[SerializeField] protected float rotationTime = 0.5f;
	[SerializeField] protected float flowThroughTime = 0.5f;
	protected bool canRotate = true;
	private AudioSource audioSource;

	[SerializeField] private bool rotateOnStart = true;

	protected bool isShaking;
	[SerializeField] private float shakeAmount = 0.005f;
	private Vector3 originalPosition;
	private Vector3 originalScale;
	[SerializeField] private float scaleAmount = 1.05f;

	[SerializeField] protected PipeEndpoint endpointA, endpointB;

	protected virtual void Start() {
		originalScale = transform.localScale;
		audioSource = GetComponent<AudioSource>();

		if (rotateOnStart) {
			int rotation = Random.Range(0, 4);
			transform.Rotate(new Vector3(0, 0, rotation * 90));
		}

		originalPosition = transform.position;
	}

	private void Update() {
		if (isShaking) {
			transform.position = originalPosition;
			transform.Translate(Random.insideUnitCircle * shakeAmount);
		}
	}

	public virtual IEnumerator FlowThrough(PipeEndpoint input) {
		canRotate = false;
		yield return new WaitForSeconds(flowThroughTime / 2);
		isShaking = true;
		yield return new WaitForSeconds(flowThroughTime / 2);
		PipeEndpoint output;
		if (input.Equals(endpointA))
			output = endpointB;
		else
			output = endpointA;
		output.OutFlow(true);
	}
	
	public virtual IEnumerator FlowBack(PipeEndpoint input) {
		yield return new WaitForSeconds(flowThroughTime / 2);
		isShaking = false;
		transform.position = originalPosition;
		yield return new WaitForSeconds(flowThroughTime / 2);
		canRotate = true;

		PipeEndpoint output;
		if (input.Equals(endpointA))
			output = endpointB;
		else
			output = endpointA;
		output.OutFlow(false);
	}

	private void OnMouseOver() {
		StartCoroutine(HighlightCoroutine());
	}

	protected virtual IEnumerator HighlightCoroutine() {
		transform.localScale *= scaleAmount;
		yield return new WaitForEndOfFrame();
		transform.localScale = originalScale;
	}

	public virtual void OnMouseDown() {
		if (canRotate)
			StartCoroutine(TurnPipe());
	}

	private IEnumerator TurnPipe() {
		canRotate = false;
		float degrees = 0;

		while(degrees < 90) {
			float turnAmount = (90 / rotationTime) * Time.deltaTime;
			transform.Rotate(new Vector3(0, 0, turnAmount));
			degrees += turnAmount;
			yield return new WaitForEndOfFrame();
		}
		//transform.eulerAngles = new Vector3(0, 0, (int) transform.eulerAngles.z);
		transform.Rotate(new Vector3(0, 0, -(transform.eulerAngles.z % 90)));
		canRotate = true;
	}
}
