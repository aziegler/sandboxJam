﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeTutorial : MonoBehaviour {

	public float delay;

	private Image _image;

	// Use this for initialization
	void Start () 
	{
		_image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public const string ACTIVTE_TUTORIAL = "ActivateTutorial";
	public void ActivateTutorial()
	{
		StopCoroutine ("Fade");
		StartCoroutine ("Fade", 1f);
	}

	public const string DESACTIVATE_TUTORIAL = "DesactivateTutorial";
	public void DesactivateTutorial ()
	{
		StopCoroutine ("Fade");
		StartCoroutine ("Fade", 0f);
	}

	IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = _image.color.a;
		float t = 0f;
		float startTime = Time.time;

		do {
			t = Mathf.Clamp ((Time.time - startTime) / delay, 0f, 1f);
			Color c = _image.color;
			c.a = Mathf.Lerp (startAlpha, targetAlpha, t);
			_image.color = c;
			yield return null;
		} while(t<1f);
	}
}
