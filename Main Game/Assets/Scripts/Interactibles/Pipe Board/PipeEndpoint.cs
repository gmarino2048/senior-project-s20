using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEndpoint : MonoBehaviour {
	private PipeEndpoint connectedNeighbor;
	private PipeSegment pipe;
	private ParticleSystem waterSprayEffect;
	[SerializeField] private float errorSprayDuration = 2;
	private AudioSource spraySound;

	private void Start() {
		pipe = GetComponentInParent<PipeSegment>();
		waterSprayEffect = GetComponent<ParticleSystem>();
		spraySound = GetComponent<AudioSource>();
	}

	public void InFlow(bool flowingForward) {
		if (flowingForward)
			StartCoroutine(pipe.FlowThrough(this));
		else
			StartCoroutine(pipe.FlowBack(this));
	}

	private IEnumerator ErrorSpray() {
		spraySound.Play();
		waterSprayEffect.Play();
		yield return new WaitForSeconds(errorSprayDuration);
		waterSprayEffect.Stop();
		StartCoroutine(pipe.FlowBack(this));
	}

	public void OutFlow(bool flowingBackward) {
		if (connectedNeighbor != null)
			connectedNeighbor.InFlow(flowingBackward);
		else
			StartCoroutine(ErrorSpray());
	}

	private void OnTriggerEnter(Collider other) {
		PipeEndpoint endpoint = other.GetComponent<PipeEndpoint>();
		if (endpoint != null)
			connectedNeighbor = endpoint;
	}
	private void OnTriggerExit(Collider other) {
		PipeEndpoint endpoint = other.GetComponent<PipeEndpoint>();
		if (endpoint != null)
			connectedNeighbor = null;
	}
}
