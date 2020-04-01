using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGoal : PipeSegment {
	[SerializeField] private GameObject gameboy;
	private PuzzleFocusManager focusManager;

	protected override void Start() {
		base.Start();
		canRotate = false;
		focusManager = GetComponentInParent<PuzzleFocusManager>();
	}

	public override IEnumerator FlowThrough(PipeEndpoint input) {
		yield return new WaitForSeconds(flowThroughTime / 2);
		isShaking = true;
		yield return new WaitForSeconds(flowThroughTime / 2);

		focusManager.ToggleState(false);
		gameboy.SetActive(true);
	}
}
