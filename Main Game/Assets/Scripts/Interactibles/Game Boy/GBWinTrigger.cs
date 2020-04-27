using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBWinTrigger : MonoBehaviour {
	[SerializeField] private SpriteRenderer winScreen;
	[SerializeField] private HingeOpener windowController;
	[SerializeField] private Collider gemPrison;
	[SerializeField] private GameObject fireflyHighlight;
	[SerializeField] private Sprite[] windowAnimationSprites;
	[SerializeField] private float animationSpeed;

	private GBFocusManager focuser;

	void Start() {
		focuser = FindObjectOfType<GBFocusManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.GetComponent<GBPlayerController>() != null) {
			winScreen.enabled = true;
			focuser.ToggleState(false);
			windowController.Open();
			fireflyHighlight.SetActive(true);
			StartCoroutine(WindowAnimation());
			gemPrison.enabled = false;
		}
	}

	private IEnumerator WindowAnimation() {
		while (true) {
			foreach (Sprite sprite in windowAnimationSprites) {
				winScreen.sprite = sprite;
				yield return new WaitForSeconds(animationSpeed);
			}
		}
	}
}
