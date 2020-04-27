using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameFlowController : MonoBehaviour
{
    [Header("Menu settings")]
    [SerializeField] private ScenicCameraRig cameraRig;
    [SerializeField] private MenuController menu;
    private Camera scenicCamera;

    [Header("Player Settings")]
    [SerializeField] private KeyboardPlayerController player;
    private Camera playerCamera;
    private Transform initialPlayerTransform;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float contemplationTime;
    private FadeController fadeController;

    private void Awake()
    {
        scenicCamera = cameraRig.GetComponentInChildren<Camera>();
        playerCamera = player.GetComponentInChildren<Camera>();
        
        initialPlayerTransform = player.gameObject.transform;

        fadeController = FindObjectOfType<FadeController>();
    }

    private void Start()
    {
        SwitchToMenu();
    }

    private void SwitchToPlayer()
    {
        playerCamera.enabled = true;

        cameraRig.active = false;
        scenicCamera.enabled = false;
        
        player.SetPlayerMovementLock(false);
    }

    private void SwitchToMenu()
    {
        cameraRig.active = true;
        scenicCamera.enabled = true;
        
        playerCamera.enabled = false;
        
        player.SetPlayerMovementLock(true);
    }

    private void ResetPlayer()
    {
        var playerObject = player.gameObject;
        playerObject.transform.position = initialPlayerTransform.position;
        playerObject.transform.rotation = initialPlayerTransform.rotation;
    }

    public IEnumerator StartGame()
    {
        yield return fadeController.FadeOut(Color.black, fadeDuration);
        SwitchToPlayer();
        yield return fadeController.FadeIn(fadeDuration);
    }

    public IEnumerator EndGame()
    {
        yield return fadeController.FadeOut(Color.white, fadeDuration);
        // TODO: CHRISTIAN PUT THE CREDITS HERE
        yield return new WaitForSeconds(contemplationTime);
        SwitchToMenu();
        ResetPlayer();
        yield return fadeController.FadeIn(fadeDuration);
    }
}
