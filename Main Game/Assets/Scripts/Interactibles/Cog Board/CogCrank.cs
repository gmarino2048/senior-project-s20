using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCrank : InteractiveObject {
	[SerializeField] private GameObject crankHandle;
	[SerializeField] private float rotationSpeed;
	private CogBoard board;
	private CogSpinner[] allCogs;
	private bool isCranking;

	[SerializeField] private PipeStarter pipeBoard;

	protected override void Start() {
		base.Start();
		board = FindObjectOfType<CogBoard>();
		allCogs = FindObjectsOfType<CogSpinner>();
	}

	void Update() {
		if (isCranking)
			crankHandle.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
	}

	public override void Interact(Transform handTF) {
		if (!isCranking) {
			StartCoroutine(CheckBoard());
		}
	}

	private IEnumerator CheckBoard() {
		isCranking = true;
		yield return new WaitForSeconds(0.1f);
		if (!board.isCogBoardSolved()) {
			isCranking = false;
		}
		else {
			foreach (CogSpinner cog in allCogs)
				cog.StartSpinning();

			//Should do... something visually here to indicate that the pipeboard is available now
			pipeBoard.ActivateStarter();
		}
	}
}
