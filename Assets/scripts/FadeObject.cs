using UnityEngine;
using System.Collections;
using System;
public class FadeObject : MonoBehaviour {

    public float Delay = 1f;
    public float InitDelay = 1f;
    public Action OnHide;

    SpriteRenderer sprite;
    float startTime;
    float startAlpha;


	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time;
        startAlpha = 1f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    var date = (Time.time - startTime);
	    float t = date < InitDelay ? 0f : Mathf.Clamp((date-InitDelay)/Delay, 0f, 1f);
        float alpha = Mathf.Lerp(startAlpha, 0f, t);
        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
        
        if(date > (InitDelay + Delay))
        {
            if(null != OnHide)
            {
                OnHide();
            }
        }
	}
}
