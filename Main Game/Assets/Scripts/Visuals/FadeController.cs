using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image blackoutSquare;

    private void Start()
    {
        SetTransparent();
    }

    public void SetTransparent()
    {
        blackoutSquare.CrossFadeAlpha(0.0f, 0.0f, true);
    }

    public IEnumerator FadeOut(Color color, float duration)
    {
        blackoutSquare.color = color;
        blackoutSquare.CrossFadeAlpha(1.0f, duration, false);
        yield return new WaitForSeconds(duration);
    }

    public IEnumerator FadeIn(float duration)
    {
        blackoutSquare.CrossFadeAlpha(0.0f, duration, false);
        yield return new WaitForSeconds(duration);
        blackoutSquare.color = Color.black;
    }
}
