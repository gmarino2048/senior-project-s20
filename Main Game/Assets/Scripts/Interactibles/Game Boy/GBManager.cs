using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBManager : MonoBehaviour {
	private GBBobo[] bobos;
	private GBPlayerController player;

	void Start() {
		bobos = GetComponentsInChildren<GBBobo>();
		player = GetComponentInChildren<GBPlayerController>();
	}

	public void TurnOn(bool turnOn) {
		player.enabled = turnOn;
		foreach (GBBobo bobo in bobos)
			bobo.enabled = turnOn;
	}
}
