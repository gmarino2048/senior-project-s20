using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBHazard : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		GBPlayerController player = collision.gameObject.GetComponent<GBPlayerController>();
		if(player != null) {
			player.Kill();
		}
	}
}
