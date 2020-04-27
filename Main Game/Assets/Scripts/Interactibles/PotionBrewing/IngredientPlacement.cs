using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPlacement : ObjectPlacementVolume
{
	[SerializeField] private Cauldron cauldron;
	public override void PlacementTrigger(InteractiveObject ingredient)
	{
		cauldron.AddIngredient(ingredient);
		StartCoroutine(PlacementAnimation(ingredient));
	}

	private IEnumerator PlacementAnimation(InteractiveObject ingredient)
	{
		for (int i = 0; i < 70; i++)
		{
			ingredient.transform.Translate(new Vector3(0, -0.01f, 0));
			yield return new WaitForFixedUpdate();
		}
		ingredient.gameObject.SetActive(false);
	}
}
