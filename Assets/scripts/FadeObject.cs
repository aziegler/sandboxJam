using UnityEngine;
using System.Collections;
using System;
public class FadeObject : MonoBehaviour {

    public float Delay = 0.4f;
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
        float t = (Time.time - startTime)/Delay;
        float alpha = Mathf.Lerp(startAlpha, 0f, t);
        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
        
        if(Mathf.Approximately(t,1f))
        {
            if(null != OnHide)
            {
                OnHide();
            }
        }
	}
}
