using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : HoldableObject
{
	[SerializeField]
	private Ingredient ingredient;

	public Ingredient GetIngredient()
	{
		return ingredient;
	}
}
