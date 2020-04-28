using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBManager : MonoBehaviour {
	[SerializeField] private GBBobo[] bobos;
	[SerializeField] private GBPlayerController player;

	void OnEnable() {
		foreach (GBBobo bobo in bobos) {
			try {
				bobo.enabled = false;
			} catch (NullReferenceException e) { }
		}
	}

	public void TurnOn(bool turnOn) {
		player.enabled = turnOn;
		//gameScreen.SetActive(turnOn);

		foreach (GBBobo bobo in bobos) {
			try {
				try {
					bobo.enabled = turnOn;
				} catch (MissingReferenceException e) { }
			} catch (NullReferenceException e) { }
		}
	}
}
