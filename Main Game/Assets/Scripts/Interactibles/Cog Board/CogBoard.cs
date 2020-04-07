using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBoard : MonoBehaviour
{
	private bool cog1 = false;
	private bool cog2 = false;
	private bool cog3 = false;
	private bool cogBoardSolved = false;

	// Start is called before the first frame update
	void Start()
	{
		CogPlacement.CogPlaced += checkCogs;
	}

	private void OnDestroy()
	{
		CogPlacement.CogPlaced -= checkCogs;
	}

	private void checkCogs(string cogPlacement, bool correctPlacement)
	{
		switch (cogPlacement)
		{
			case "placement1":
				cog1 = correctPlacement;
				break;
			case "placement2":
				cog2 = correctPlacement;
				break;
			case "placement3":
				cog3 = correctPlacement;
				break;
		}

		cogBoardSolved = cog1 && cog2 && cog3;
		Debug.Log("Cog board solved: " + cogBoardSolved);
	}

	public bool isCogBoardSolved()
	{
		return cogBoardSolved;
	}
}
