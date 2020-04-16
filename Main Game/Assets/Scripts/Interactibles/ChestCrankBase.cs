using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrankBase : ObjectPlacementVolume {
	[SerializeField] private Transform crankTF;

	public override void PlacementTrigger(InteractiveObject gameObject) {
		gameObject.transform.position = crankTF.position;
		gameObject.transform.rotation = crankTF.rotation;
		gameObject.transform.localScale = crankTF.localScale;

		gameObject.GetComponent<Collider>().enabled = true;
		gameObject.GetComponent<ChestCrankHandle>().isAttached = true;
		Destroy(this);
	}
}
