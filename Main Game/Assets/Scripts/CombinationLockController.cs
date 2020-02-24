using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockController : MonoBehaviour
{
    [SerializeField]
    public int[] correctCombination = new int[5];
    public int[] currentCombination = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        Rotate.Rotated += CheckCombination;
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

        bool unlocked = true;

        for (int i = 0; i < correctCombination.Length; i++){
            if (correctCombination[i] != currentCombination[i]){
                unlocked = false;
            }
        }

        if (unlocked){
            Debug.Log("Unlocked.");
        }
    }

    private void OnDestroy() {
        Rotate.Rotated -= CheckCombination;
    }
}
