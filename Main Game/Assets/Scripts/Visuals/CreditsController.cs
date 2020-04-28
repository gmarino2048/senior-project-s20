using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
	[SerializeField] private GameObject credits;
	public GameObject start;
	public GameObject end;

	private GameFlowController flowController;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			StartCoroutine(RollCredits());
		}
	}

	private void Awake()
	{
		flowController = FindObjectOfType<GameFlowController>();
	}

	public IEnumerator RollCredits()
	{
		Debug.Log("Rolling credits");
		RectTransform creditPosition = credits.GetComponent<RectTransform>();
		Vector3 originalPosition = creditPosition.transform.position;

		while(Vector3.Distance(creditPosition.transform.position, end.transform.position) > 0)
		{
			creditPosition.transform.position = Vector3.Lerp(creditPosition.transform.position, end.transform.position, .001f);
			yield return new WaitForFixedUpdate();
		}
	}
}
