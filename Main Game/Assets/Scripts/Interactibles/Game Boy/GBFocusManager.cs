using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBFocusManager : PuzzleFocusManager {
	[SerializeField] private GBManager manager;

	public override void ToggleState(bool enable) {
		base.ToggleState(enable);
		manager.TurnOn(enable);
	}
}
