using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBWinTrigger : MonoBehaviour {
	[SerializeField] private GameObject winScreen;
	[SerializeField] private HingeOpener windowController;
	private GBFocusManager focuser;

	void Start() {
		focuser = FindObjectOfType<GBFocusManager>();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponent<GBPlayerController>() != null) {
			winScreen.SetActive(true);
			focuser.ToggleState(false);
			windowController.Open();
			Destroy(this.gameObject);
		}
	}
}
