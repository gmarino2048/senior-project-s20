﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBoardController : MonoBehaviour
{
	[SerializeField] private GameObject[] currentCogOrderSlots;
	[SerializeField] private GameObject[] correctCogOrder;

	private void Start()
	{
		CogController.Connected += CheckCogOrder;
	}

	/*private void Update()
	{
		CheckCogOrder();
	}*/

	private void CheckCogOrder(int cogID, GameObject cog)
	{
		switch (name)
		{
			case "Cog1":
				currentCombination[0] = number;
				break;
			case "Cog2":
				currentCombination[1] = number;
				break;
			case "Cog3":
				currentCombination[2] = number;
				break;
		}

		bool unlocked = true;

		for (int i = 0; i < correctCombination.Length; i++)
		{
			if (correctCombination[i] != currentCombination[i])
			{
				unlocked = false;
			}
		}

		if (unlocked)
		{
			Debug.Log("Unlocked.");
		}
	}

	/*private void CheckCogOrder()
	{
		Debug.Log("Checking cog order");
		bool isSolved = true;

		for(int i = 0; i < currentCogOrderSlots.Length; i++)
		{
			if(currentCogOrderSlots[i].gameObject != correctCogOrder[i])
			{
				isSolved = false;
				Debug.Log("Slot object: " + currentCogOrderSlots[i].GetComponentsInChildren<CogController>());
				Debug.Log("Correct object: " + correctCogOrder[i].name);
			}
		}

		if (isSolved)
		{
			Unlock();
		}
	}*/

	private void Unlock()
	{
		Debug.Log("Unlocked");
	}
}