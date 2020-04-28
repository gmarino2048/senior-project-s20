using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private GameFlowController flowController;

    private void Awake()
    {
        flowController = FindObjectOfType<GameFlowController>();
        playButton.onClick.AddListener(OnPlay);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnPlay()
    {
        StartCoroutine(
            flowController.StartGame());
    }

    private void OnQuit()
    {
        Application.Quit(0);
    }
}
