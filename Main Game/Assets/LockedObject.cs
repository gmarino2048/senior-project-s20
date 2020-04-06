using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : InteractiveObject
{
	[SerializeField]
	private bool locked;
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		KeyObject.Unlocked += checkUnlocked;
    }

	private void checkUnlocked(bool unlocked, GameObject lockedObject)
	{
		if(gameObject == lockedObject)
		{
			locked = false;
			Debug.Log("Unlocked " + lockedObject.name);
		}
	}

	public bool isLocked()
	{
		return locked;
	}

	public override void Interact(Transform handTF)
	{
		Debug.Log("Lock is " + locked);
	}
}
