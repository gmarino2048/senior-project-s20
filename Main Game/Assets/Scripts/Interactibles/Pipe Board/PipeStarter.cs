using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeStarter : PipeSegment {
	private bool flowing;
	private bool waterCanFlow;
	[SerializeField] private float leverTurnDuration, leverTurnAngle;
	[SerializeField] private Transform leverTF;

	protected override void Start() {
		base.Start();
	}

	//Called by the cogboard puzzle to turn this whole puzzle on. Does not start water flow through pipes though
	public void ActivateStarter() {
		isShaking = true;
		waterCanFlow = true;
	}

	public override void OnMouseDown() {
		if (!flowing && waterCanFlow) {
			StartCoroutine(FlowThrough(null));
			StartCoroutine(TurnLever());
		}
	}

	public override IEnumerator FlowThrough(PipeEndpoint input) {
		flowing = true;
		isShaking = true;
		yield return new WaitForSeconds(flowThroughTime);
		endpointA.OutFlow(true);
	}

	public override IEnumerator FlowBack(PipeEndpoint input) {
		yield return new WaitForSeconds(flowThroughTime / 4);
		flowing = false;
		StartCoroutine(TurnLever());
	}

	private IEnumerator TurnLever() {
		float timer = 0;
		while (timer < leverTurnDuration) {
			timer += Time.deltaTime;
			leverTF.Rotate(new Vector3((Time.deltaTime / leverTurnDuration) * leverTurnAngle, 0, 0));
			yield return new WaitForEndOfFrame();
		}
		leverTurnAngle *= -1;
	}
}
