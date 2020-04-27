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
    private FadeController fadeController;

    private void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
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
            StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return fadeController.FadeOut(Color.white, fadeDuration);
        Debug.Log("Game has ended. Thanks for playing!");
        yield return new WaitForSeconds(contemplationTime);
        Application.Quit(0);
    }
}
