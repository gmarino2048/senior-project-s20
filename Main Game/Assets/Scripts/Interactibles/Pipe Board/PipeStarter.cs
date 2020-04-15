using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeStarter : PipeSegment {
	private bool flowing;
	private bool waterCanFlow;

	protected override void Start() {
		base.Start();
	}

	//Called by the cogboard puzzle to turn this whole puzzle on. Does not start water flow through pipes though
	public void ActivateStarter() {
		isShaking = true;
		waterCanFlow = true;
	}

	public override void OnMouseDown() {
		if(!flowing && waterCanFlow)
			StartCoroutine(FlowThrough(null));
	}

	public override IEnumerator FlowThrough(PipeEndpoint input) {
		flowing = true;
		isShaking = true;
		yield return new WaitForSeconds(flowThroughTime);
		endpointA.OutFlow(true);
	}

	public override IEnumerator FlowBack(PipeEndpoint input) {
		yield return new WaitForSeconds(flowThroughTime / 4);
		isShaking = false;
		flowing = false;
	}
}
