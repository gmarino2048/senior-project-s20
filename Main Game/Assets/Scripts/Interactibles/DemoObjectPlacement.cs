using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoObjectPlacement : ObjectPlacementVolume {
	public override void PlacementTrigger(InteractiveObject interactiveObject) {
		StartCoroutine(PlacementAnimation());
	}

	private IEnumerator PlacementAnimation() {
		yield return new WaitForSeconds(0.5f);
		for(int i = 0; i < 60; i++) {
			requiredObject.transform.Translate(new Vector3(0.0025f, 0, 0));
			yield return new WaitForFixedUpdate();
		}

		Transform wholeObject = transform.parent;
		while (true) {
			wholeObject.Rotate(new Vector3(0.5f, 0, 0));
			yield return new WaitForFixedUpdate();
		}
	}
}
