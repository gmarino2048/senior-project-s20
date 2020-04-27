using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour
{
    public enum GemColor
    {
        Red,
        Green,
        Blue
    }

    private bool _red;
    private bool _green;
    private bool _blue;

    [SerializeField] private float fadeDuration;
    [SerializeField] private float contemplationTime;

    private GameFlowController flowController;
    private FadeController fadeController;

    private void Awake()
    {
        fadeController = FindObjectOfType<FadeController>();
        flowController = FindObjectOfType<GameFlowController>();
    }

    public void AddGem(GemColor color)
    {
        switch (color)
        {
            case GemColor.Red:
                _red = true;
                break;
            case GemColor.Green:
                _green = true;
                break;
            case GemColor.Blue:
                _blue = true;
                break;
            default:
                throw new InvalidOperationException("Unrecognized gem color");
        }

        if (_red && _green && _blue)
            StartCoroutine(flowController.EndGame());
    }
}
