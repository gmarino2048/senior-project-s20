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

	[SerializeField] private float creditsScrollTime, distanceToTravel;

	private void Awake()
	{
		flowController = FindObjectOfType<GameFlowController>();
	}

	public IEnumerator RollCredits()
	{
		RectTransform creditPosition = credits.GetComponent<RectTransform>();

		float timer = 0;

		while(timer < creditsScrollTime)
		{
			creditPosition.Translate(new Vector3(0, Time.deltaTime * (distanceToTravel / creditsScrollTime)));
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
