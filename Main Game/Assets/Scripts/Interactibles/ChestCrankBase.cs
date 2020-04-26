using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrankBase : ObjectPlacementVolume {
	[SerializeField] private Transform crankTF;
	[SerializeField] private GameObject turnableCrank;

	public override void PlacementTrigger(InteractiveObject placedObject) {
		placedObject.transform.position = crankTF.position;
		placedObject.transform.rotation = crankTF.rotation;
		placedObject.transform.localScale = crankTF.localScale;

		placedObject.gameObject.SetActive(false);
		turnableCrank.SetActive(true);

		Destroy(this);
	}
}
