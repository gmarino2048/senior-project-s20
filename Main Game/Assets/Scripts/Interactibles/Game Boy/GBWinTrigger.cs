using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBWinTrigger : MonoBehaviour {
	[SerializeField] private GameObject winScreen;
	private TreehouseWindow windowController;
	private GBFocusManager focuser;

	void Start() {
		windowController = FindObjectOfType<TreehouseWindow>();
		focuser = FindObjectOfType<GBFocusManager>();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponent<GBPlayerController>() != null) {
			winScreen.SetActive(true);
			focuser.ToggleState(false);
			windowController.OpenWindow();
			Destroy(this.gameObject);
		}
	}
}
