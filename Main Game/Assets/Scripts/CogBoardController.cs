using System;
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

	private void CheckCogOrder()
	{
		Debug.Log("Checking cog order");
		bool isSolved = true;

		for(int i = 0; i < currentCogOrderSlots.Length; i++)
		{
			if(currentCogOrderSlots[i] != correctCogOrder[i])
			{
				isSolved = false;
				Debug.Log("Locked");
			}
		}

		if (isSolved)
		{
			Unlock();
		}
	}

	private void Unlock()
	{
		Debug.Log("Unlocked");
	}
}
