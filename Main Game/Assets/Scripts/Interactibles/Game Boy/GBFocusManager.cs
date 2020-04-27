using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBFocusManager : PuzzleFocusManager {
	[SerializeField] private GBPlayerController gbController;

	public override void ToggleState(bool enable) {
		base.ToggleState(enable);
		gbController.SetControlState(enable);
	}
}
