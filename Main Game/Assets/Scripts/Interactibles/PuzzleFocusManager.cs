using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This lets the player click on a puzzle to lock the camera to it
//Put this on an invisible collider that encompasses the whole puzzle

public class PuzzleFocusManager : InteractiveObject {
	[SerializeField] private GameObject fixedCamera;

	private Collider triggerVolume;
	private KeyboardPlayerController player;
	private bool isActive = false;

	protected override void Start() {
		base.Start();
		triggerVolume = GetComponent<Collider>();
		player = FindObjectOfType<KeyboardPlayerController>();
		InteractionIsHeld = false;
		if (fixedCamera == null)
			fixedCamera = GetComponentInChildren<Camera>().gameObject;
	}

	public override void Interact(Transform handTF) {
		Debug.Log("togling");
		ToggleState(true);
	}

	void Update() {
		if (isActive) {
			if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) {
				ToggleState(false);
			}
		}
	}

	public void ToggleState(bool enable) {
		player.SetPlayerMovementLock(enable);
		player.SetCameraState(!enable);
		triggerVolume.enabled = !enable;
		fixedCamera.SetActive(enable);
		isActive = enable;
	}
}
