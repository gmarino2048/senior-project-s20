using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransitionManager : MonoBehaviour
{
    private Camera _playerCamera;
    private MagnifyingGlassManager _magnifyingGlass;
    private TransitionTarget _target;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize elements
        _playerCamera = gameObject.GetComponentInChildren<Camera>();
        _magnifyingGlass = gameObject.GetComponentInChildren<MagnifyingGlassManager>();

        _target = null;
    }

    // Update is called once per frame
    void Update()
    {
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
                
                _target = sceneTransition.Target;
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
}
