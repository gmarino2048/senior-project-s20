using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest1Manager1 : PotionInteractionObject
{
	[SerializeField]
	private GameObject ObjectsToActivate;

	public override IEnumerator PotionEffects()
	{
		Debug.Log("Should be enabling");
		ObjectsToActivate.SetActive(true);
		this.gameObject.SetActive(false);
		yield return new WaitForSeconds(0);
	}
}
