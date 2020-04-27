using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBManager : MonoBehaviour {
	[SerializeField] private GBBobo[] bobos;
	[SerializeField] private GBPlayerController player;

	void OnEnable() {
		foreach (GBBobo bobo in bobos)
			bobo.enabled = false;
	}

	public void TurnOn(bool turnOn) {
		player.enabled = turnOn;
		//gameScreen.SetActive(turnOn);
		try {
			foreach (GBBobo bobo in bobos)
				bobo.enabled = turnOn;
		} catch (MissingReferenceException e) { }
	}
}
