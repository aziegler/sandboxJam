﻿using UnityEngine;
using System.Collections;

public class AnimationSkip : MonoBehaviour {
	bool _skiped = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.anyKeyDown)
		{
			if(!_skiped)
			{
				_skiped = true;
				StartCoroutine("SkipAnimation");
			}
			else
			{
				Application.LoadLevel(1);
			}
		}
	}

	IEnumerator SkipAnimation()
	{
		Animator[] animators = GameObject.FindObjectsOfType<Animator> ();
		
		foreach(Animator animator in animators)
		{
			animator.speed = 10000f;
		}

		yield return null;

		foreach(Animator animator in animators)
		{
			animator.speed = 1f;
		}
	}
}
