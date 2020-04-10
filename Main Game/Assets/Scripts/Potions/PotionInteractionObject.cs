using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionInteractionObject : MonoBehaviour {
	public PotionType requiredEffect;

	public virtual void HitByPotion() {
		StartCoroutine(PotionEffects());
	}

	abstract public IEnumerator PotionEffects();
}
