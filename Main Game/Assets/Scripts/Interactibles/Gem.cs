using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : HoldableObject
{
	[SerializeField] private GameObject linkedGem;

	public override void Interact(Transform handTF) {
		base.Interact(handTF);
		if(linkedGem != null) {
			Destroy(linkedGem);
			linkedGem = null;
		}
	}
}
