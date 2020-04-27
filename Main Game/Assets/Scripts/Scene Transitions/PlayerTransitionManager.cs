using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Visuals;

public class PlayerTransitionManager : MonoBehaviour
{
	private Camera playerCamera;
	private MagnifyingGlassManager magnifyingGlass;
	private Flicker campfire;

	[SerializeField] private float fadeDuration;
	private FadeController fadeController;

	// Start is called before the first frame update
	private void Start()
	{
		// Initialize elements
		playerCamera = gameObject.GetComponentInChildren<Camera>();
		magnifyingGlass = gameObject.GetComponentInChildren<MagnifyingGlassManager>();
		fadeController = FindObjectOfType<FadeController>();
	}

	// Update is called once per frame
	private void Update()
	{
		// If mag glass enabled, cast out from camera
		if (magnifyingGlass.isEnabled)
		{
			var cameraTransform = playerCamera.transform;

			// Maybe I could do this with triggers
			// Better stick to what I know for now
			var result = Physics.Raycast(
				cameraTransform.position,
				cameraTransform.TransformDirection(Vector3.forward),
				out var hit
			);

			if (result)
			{
				var compareObject = hit.collider.gameObject;
				var sceneTransition = compareObject.GetComponent<SceneTransitionZone>();

				if (sceneTransition == null)
				{
					magnifyingGlass.SetTransitionDisabled();
					return;
				}
				
				magnifyingGlass.SetTransitionEnabled();

				// Grab primary mouse button
				if (Input.GetMouseButtonDown(0))
				{
					StartCoroutine(DoTransition(sceneTransition));
					campfire.ResetIntensity();
				}
			}
			else
			{
				magnifyingGlass.SetTransitionDisabled();
			}
		}
		else
		{
			// Maybe I should remove this for performance?
			magnifyingGlass.SetTransitionDisabled();
		}
	}

	private IEnumerator DoTransition(SceneTransitionZone sceneTransition)
	{
		yield return fadeController.FadeOut(Color.black, fadeDuration);
		gameObject.transform.position = sceneTransition.target.spawnPoint;
		yield return fadeController.FadeIn(fadeDuration);
	}
}
