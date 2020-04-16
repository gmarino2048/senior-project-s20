using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagnifyingGlassManager : MonoBehaviour
{
	private const string ColorName = "_Color";
	private const string EmissionColorName = "_EmissionColor";
	
	[SerializeField] private GameObject magnifyingGlassObject;
	public bool isEnabled { get; private set; }

	[SerializeField] private float rotationDuration;
	private bool isAnimating = false;

	[SerializeField] private Color inactiveColor;
	[SerializeField] private Color sceneTransitionColor;
	[SerializeField] private float colorSwapDuration;

	[SerializeField] private GameObject lens;
	private Color _currentColor;
	private Renderer _renderer;
	private Coroutine _colorCoroutine;		// Only allow one coroutine to run at a time		

	private void Start()
	{
		_renderer = lens.GetComponent<Renderer>();
		SetShaderColor(inactiveColor);
	}

	public void Toggle() {
		if (!isAnimating) {
			isAnimating = true;
			StartCoroutine(ToggleAnimation(!isEnabled));
		}
	}

	private IEnumerator ToggleAnimation(bool doEnable) {
		float timer = 0;
		int direction;
		isAnimating = true;

		if (doEnable) {
			magnifyingGlassObject.SetActive(true);
			direction = -1;
		}
		else {
			direction = 1;
		}
			
		while (timer < rotationDuration) {
			transform.Rotate(new Vector3(0, 0, Time.deltaTime * (90 / rotationDuration) * direction));
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		isEnabled = doEnable;
		//The rotation above is slightly imprecise, so this resets it to the exact angle we want
		if (doEnable) {
			transform.rotation = new Quaternion();
			transform.Rotate(new Vector3(0, 0, 180));
		}
		else {
			magnifyingGlassObject.SetActive(false);
		}

		isAnimating = false;
	}
	
	public void SetTransitionEnabled()
	{
		if (_colorCoroutine != null)
			StopCoroutine(_colorCoroutine);

		_colorCoroutine = StartCoroutine(ChangeColor(sceneTransitionColor));
	}

	public void SetTransitionDisabled()
	{
		if (_colorCoroutine != null)
			StopCoroutine(_colorCoroutine);

		_colorCoroutine = StartCoroutine(ChangeColor(inactiveColor));
	}

	private IEnumerator ChangeColor(Color newColor)
	{
		float[] rates =
		{
			(newColor.r - _currentColor.r) / colorSwapDuration,
			(newColor.g - _currentColor.g) / colorSwapDuration,
			(newColor.b - _currentColor.b) / colorSwapDuration,
			(newColor.a - _currentColor.a) / colorSwapDuration
		};

		var accTime = 0.0f;
		while (accTime < colorSwapDuration)
		{
			_currentColor = new Color(
				_currentColor.r + rates[0] * Time.deltaTime,
				_currentColor.g + rates[1] * Time.deltaTime,
				_currentColor.b + rates[2] * Time.deltaTime,
				_currentColor.a + rates[3] * Time.deltaTime
			);
			
			accTime += Time.deltaTime;
			SetShaderColor(_currentColor);
			
			yield return new WaitForEndOfFrame();
		}

		_colorCoroutine = null;
	}

	private void SetShaderColor(Color color)
	{
		_renderer.sharedMaterial.SetColor(EmissionColorName, color);
	}
}
