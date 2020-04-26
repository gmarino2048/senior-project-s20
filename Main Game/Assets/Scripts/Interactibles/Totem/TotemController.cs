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
        // Do nothing yet...
        Debug.Log("End game");
        yield return null;
    }
}
