using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockController : MonoBehaviour
{
    public int[] correctCombination = new int[5];
    public int[] currentCombination = new int[5];
	public GameObject suitCaseTop;
    // Start is called before the first frame update
    void Start()
    {
        Wheel.Rotated += CheckCombination;
    }

    private void CheckCombination(string wheel, int number){
        switch(wheel){
            case "Wheel1":
                currentCombination[0] = number;
                break;
            case "Wheel2":
				currentCombination[1] = number;
                break;
            case "Wheel3":
                currentCombination[2] = number;
                break;
            case "Wheel4":
                currentCombination[3] = number;
                break;
            case "Wheel5":
                currentCombination[4] = number;
                break;
        }

        for (int i = 0; i < correctCombination.Length; i++)
		{
			if (correctCombination[i] != currentCombination[i]){
                return; // Immediately return since the combination is wrong and the suitcase should not be unlocked.
            }
		}

		StartCoroutine(Unlock());
    }

    private void OnDestroy() {
        Wheel.Rotated -= CheckCombination;
    }

	private IEnumerator Unlock()
	{
		Debug.Log("Unlocked");
		for (int i = 0; i < 15; i++)
		{
			suitCaseTop.transform.Rotate(0f, 0f, -7f);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
