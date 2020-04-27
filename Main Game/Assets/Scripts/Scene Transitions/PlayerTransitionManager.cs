using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Visuals;

public class PlayerTransitionManager : MonoBehaviour
{
	private Camera _playerCamera;
	private MagnifyingGlassManager _magnifyingGlass;
	private Flicker _campfire;

	// Start is called before the first frame update
	private void Start()
	{
		// Initialize elements
		_playerCamera = gameObject.GetComponentInChildren<Camera>();
		_magnifyingGlass = gameObject.GetComponentInChildren<MagnifyingGlassManager>();
		_campfire = FindObjectOfType<Flicker>();
	}

	// Update is called once per frame
	private void Update()
	{
		// If mag glass enabled, cast out from camera
		if (_magnifyingGlass.isEnabled)
		{
			var cameraTransform = _playerCamera.transform;

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
					_magnifyingGlass.SetTransitionDisabled();
					return;
				}
				
				_magnifyingGlass.SetTransitionEnabled();

				// Grab primary mouse button
				if (Input.GetMouseButtonDown(0)) {
					gameObject.transform.position = sceneTransition.target.spawnPoint;
					_campfire.ResetIntensity();
				}
			}
			else
			{
				_magnifyingGlass.SetTransitionDisabled();
			}
		}
		else
		{
			// Maybe I should remove this for performance?
			_magnifyingGlass.SetTransitionDisabled();
		}
	}
}
