using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerTransitionManager : MonoBehaviour
{
    private Camera _playerCamera;
    private MagnifyingGlassManager _magnifyingGlass;
    private TransitionTarget _target;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize elements
        _playerCamera = gameObject.GetComponentInChildren<Camera>();
        _magnifyingGlass = gameObject.GetComponentInChildren<MagnifyingGlassManager>();

        _target = null;
    }

    // Update is called once per frame
    private void Update()
    {
        // Grab primary mouse button
        if (Input.GetMouseButtonDown(0))
        {
            HandleMousePressed();
        }
        
        // If mag glass enabled, cast out from camera
        if (_magnifyingGlass.isEnabled)
        {
            var cameraTransform = _playerCamera.gameObject.transform;

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
                    _target = null;
                    _magnifyingGlass.SetTransitionDisabled();
                    
                    return;
                }
                
                _target = sceneTransition.target;
                _magnifyingGlass.SetTransitionEnabled();
            }
            else
            {
                _target = null; // Don't forget to reset the target
                _magnifyingGlass.SetTransitionDisabled();
            }
        }
        else
        {
            _target = null;
            // Maybe I should remove this for performance?
            _magnifyingGlass.SetTransitionDisabled();
        }
    }

    private void HandleMousePressed()
    {
        // Bail if no transition target
        if (_target == null) return;
        
        var spawnPosition = _target.spawnPoint;
        gameObject.transform.position = spawnPosition;
    }
}
