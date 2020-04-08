using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionInteractionObject : MonoBehaviour {
	public PotionType requiredEffect;

	abstract public IEnumerator HitByPotion();
}
