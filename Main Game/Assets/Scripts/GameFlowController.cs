using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameFlowController : MonoBehaviour
{
    [Header("Menu settings")]
    [SerializeField] private ScenicCameraRig cameraRig;
    [SerializeField] private MenuController menu;
	[SerializeField] private CreditsController credits;
    private Camera scenicCamera;
    private AudioListener scenicAudioListener;
    private Canvas canvas;

    [Header("Player Settings")]
    [SerializeField] private KeyboardPlayerController player;
    private Camera playerCamera;
    private AudioListener playerAudioListener;
    private Transform initialPlayerTransform;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float contemplationTime;
    private FadeController fadeController;

    private void Awake()
    {
        scenicCamera = cameraRig.GetComponentInChildren<Camera>();
        scenicAudioListener = scenicCamera.GetComponent<AudioListener>();
        playerCamera = player.GetComponentInChildren<Camera>();
        playerAudioListener = playerCamera.GetComponent<AudioListener>();
        canvas = menu.GetComponent<Canvas>();
        
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
        playerAudioListener.enabled = true;
        cameraRig.active = false;
        scenicCamera.enabled = false;
        scenicAudioListener.enabled = false;

        player.SetPlayerMovementLock(false);
    }

    private void SwitchToMenu()
    {
        cameraRig.active = true;
        playerAudioListener.enabled = true;
        scenicCamera.enabled = true;

        playerCamera.enabled = false;
        playerAudioListener.enabled = false;
        
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
        canvas.enabled = false;
        yield return fadeController.FadeOut(Color.black, fadeDuration);
        SwitchToPlayer();
        yield return fadeController.FadeIn(fadeDuration);
    }

    public IEnumerator EndGame()
    {
        yield return fadeController.FadeOut(Color.white, fadeDuration);
		// TODO: CHRISTIAN PUT THE CREDITS HERE
		StartCoroutine(credits.RollCredits());
		//yield return credits.RollCredits();
        yield return new WaitForSeconds(contemplationTime);
        Application.Quit();
    }
}
