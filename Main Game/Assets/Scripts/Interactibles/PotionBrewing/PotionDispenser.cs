using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDispenser : MonoBehaviour
{
	[SerializeField]
	public GameObject corrosivePotion;
	public GameObject growPotion;
	public GameObject burnPotion;
	public GameObject freezePotion;
	public GameObject explodePotion;

	public GameObject corrosivePotionDispenser;
	public GameObject growPotionDispenser;
	public GameObject burnPotionDispenser;
	public GameObject freezePotionDispenser;
	public GameObject explodePotionDispenser;

	private void Start()
	{
		Cauldron.Brewed += EnablePotionDispenser;
	}

	public void EnablePotionDispenser(string potionName)
	{
		switch (potionName)
		{
			case "corrosive":
				Instantiate(corrosivePotion, corrosivePotionDispenser.transform);
				break;
			case "growPlant":
				Instantiate(growPotion, growPotionDispenser.transform);
				break;
			case "burn":
				Instantiate(burnPotion, burnPotionDispenser.transform);
				break;
			case "freeze":
				Instantiate(freezePotion, freezePotionDispenser.transform);
				break;
			case "explode":
				Instantiate(explodePotion, explodePotionDispenser.transform);
				break;
			default:
				Debug.Log("Bad potion passed to potion dispenser.");
				break;
		}
		Debug.Log("Enabling " + potionName + " potion.");
	}
}
