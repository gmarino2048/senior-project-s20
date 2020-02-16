using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRInputManager : MonoBehaviour {
	public SteamVR_Action_Boolean triggerAction, gripAction, menuAction;
	public SteamVR_Action_Boolean leftClickAction, rightClickAction, upClickAction, downClickAction;

	public bool GetTrigger() {
		return GetTrigger(SteamVR_Input_Sources.Any);
	}
	public bool GetTrigger(SteamVR_Input_Sources hand) {
		bool mouseClick = false;
		switch (hand) {
			case SteamVR_Input_Sources.LeftHand:
				mouseClick = Input.GetMouseButton(0);
				break;
			case SteamVR_Input_Sources.RightHand:
				mouseClick = Input.GetMouseButton(1);
				break;
			case SteamVR_Input_Sources.Any:
				mouseClick = Input.GetMouseButton(0) || Input.GetMouseButton(1);
				break;
		}

		return triggerAction.GetStateDown(hand) || mouseClick;
	}
	public bool GetGrip() {
		return gripAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetGrip(SteamVR_Input_Sources hand) {
		return gripAction.GetStateDown(hand);
	}
	public bool GetMenu() {
		return menuAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetMenu(SteamVR_Input_Sources hand) {
		return menuAction.GetStateDown(hand);
	}

	public bool GetLeftClick() {
		return leftClickAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetLeftClick(SteamVR_Input_Sources hand) {
		return leftClickAction.GetStateDown(hand);
	}
	public bool GetRightClick() {
		return rightClickAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetRightClick(SteamVR_Input_Sources hand) {
		return rightClickAction.GetStateDown(hand);
	}
	public bool GetUpClick() {
		return upClickAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetUpClick(SteamVR_Input_Sources hand) {
		return upClickAction.GetStateDown(hand);
	}
	public bool GetDownClick() {
		return downClickAction.GetStateDown(SteamVR_Input_Sources.Any);
	}
	public bool GetDownClick(SteamVR_Input_Sources hand) {
		return downClickAction.GetStateDown(hand);
	}
}