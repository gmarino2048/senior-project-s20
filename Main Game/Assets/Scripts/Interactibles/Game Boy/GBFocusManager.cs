using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBFocusManager : PuzzleFocusManager {
	[SerializeField] private GBDemoCube cubeController;

	public override void ToggleState(bool enable) {
		base.ToggleState(enable);
		cubeController.SetControlState(enable);
	}
}
