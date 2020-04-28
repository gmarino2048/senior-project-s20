using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
	public static event Action<string> Brewed = delegate { };


	public List<Ingredient> Corrosive;
	public List<Ingredient> GrowPlant;
	public List<Ingredient> Burn;
	public List<Ingredient> Freeze;
	public List<Ingredient> Explode;

	private static Potion corrosivePotion = new Potion("corrosive");
	private static Potion growPlantPotion = new Potion("growPlant");
	private static Potion burnPotion = new Potion("burn");
	private static Potion freezePotion = new Potion("freeze");
	private static Potion explodePotion = new Potion("explode");

    [SerializeField] private ParticleSystem successParticles;
	[SerializeField] private AudioSource successSound;
    [SerializeField] private ParticleSystem failureParticles;
	[SerializeField] private AudioSource failureSound;

	private void Start()
	{
		SetUpPotion(corrosivePotion, Corrosive);
		SetUpPotion(growPlantPotion, GrowPlant);
		SetUpPotion(burnPotion, Burn);
		SetUpPotion(freezePotion, Freeze);
		SetUpPotion(explodePotion, Explode);
	}

	private void SetUpPotion(Potion potion, List<Ingredient> ingredientList)
	{
		foreach (Ingredient ingredient in ingredientList)
		{
			potion.IncrementIngredientAmount(ingredient);
		}
	}

	private static List<Ingredient> currentBrew = new List<Ingredient>();

	public void AddIngredient(InteractiveObject ingredient)
	{
		currentBrew.Add(ingredient.GetComponent<IngredientObject>().GetIngredient());
		CheckBrew();
	}

	private void CheckBrew()
	{
		Potion currentPotion = new Potion("current potion");
		// Only check the brew if we have 3 ingredients in th cauldron
		if (currentBrew.Count == 3)
		{
			foreach (Ingredient ingredient in currentBrew)
			{
				currentPotion.IncrementIngredientAmount(ingredient);
			}
			BrewPotion(currentPotion);
			currentBrew.Clear(); // Empty out the cauldron since a potion has been brewed
		}
	}

	private void BrewPotion(Potion currentPotion)
	{
		Potion newPotion;
		if (currentPotion.Equals(corrosivePotion))
		{
			newPotion = corrosivePotion;
            successParticles.Play();
			successSound.Play();
		}
		else if (currentPotion.Equals(growPlantPotion))
		{
			newPotion = growPlantPotion;
            successParticles.Play();
			successSound.Play();
		}
		else if (currentPotion.Equals(burnPotion))
		{
			newPotion = burnPotion;
            successParticles.Play();
			successSound.Play();
		}
		else if (currentPotion.Equals(freezePotion))
		{
			newPotion = freezePotion;
            successParticles.Play();
			successSound.Play();
		}
		else if (currentPotion.Equals(explodePotion))
		{
			newPotion = explodePotion;
            successParticles.Play();
			successSound.Play();
		}
		else
		{
			newPotion = new Potion("Bad potion");
            failureParticles.Play();
			failureSound.Play();
		}

		Debug.Log("New potion is determined on line 87 of Couldron.cs: " + newPotion.name);
		Brewed(newPotion.name);
	}




	public class Potion
	{
		public string name;

		public int eyeball = 0;
		public int spicyPepper = 0;
		public int fertilizer = 0;
		public int mushroom = 0;
		public int batWing = 0;

		public Potion(string name)
		{
			this.name = name;
		}
		public void IncrementIngredientAmount(Ingredient ingredient)
		{
			switch (ingredient)
			{
				case Ingredient.BatWing:
					batWing++;
					break;
				case Ingredient.Eyeball:
					eyeball++;
					break;
				case Ingredient.Fertilizer:
					fertilizer++;
					break;
				case Ingredient.Mushroom:
					mushroom++;
					break;
				case Ingredient.SpicyPepper:
					spicyPepper++;
					break;
				default:
					break;
			}
		}
		public bool Equals(Potion potion)
		{
			bool equalPotion = true;
			if(eyeball != potion.eyeball)
			{
				equalPotion = false;
			}
			else if(spicyPepper != potion.spicyPepper)
			{
				equalPotion = false;
			}
			else if (fertilizer != potion.fertilizer)
			{
				equalPotion = false;
			}
			else if (mushroom != potion.mushroom)
			{
				equalPotion = false;
			}
			else if (batWing != potion.batWing)
			{
				equalPotion = false;
			}
			return equalPotion;
		}
	}
}
