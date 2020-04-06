using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeStarter : PipeSegment {
	private PipeEndpoint flowStartpoint;
	private bool flowing;

	protected override void Start() {
		base.Start();
		flowStartpoint = GetComponentInChildren<PipeEndpoint>();
	}

	public override void OnMouseDown() {
		if(!flowing)
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
