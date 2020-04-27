using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBWinTrigger : MonoBehaviour {
	[SerializeField] private GameObject winScreen;
	[SerializeField] private HingeOpener windowController;
	[SerializeField] private Collider gemPrison;
	private GBFocusManager focuser;

	void Start() {
		focuser = FindObjectOfType<GBFocusManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.GetComponent<GBPlayerController>() != null) {
			winScreen.SetActive(true);
			focuser.ToggleState(false);
			windowController.Open();
			gemPrison.enabled = false;
			Destroy(this.gameObject);
		}
	}
}
