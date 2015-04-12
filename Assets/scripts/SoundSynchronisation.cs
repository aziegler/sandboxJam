using UnityEngine;
using System.Collections;

public class SoundSynchronisation : MonoBehaviour
{

    public AudioSource source;
    private float previousTime;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
        
	    if (source.time < previousTime)
	    {
	        previousTime = source.time;	        
	        return;
        }

	   /* var soundCloud = GameObject.FindGameObjectWithTag("SoundSync").GetComponent<SoundCloud>();
        soundCloud.ResyncSound(source.time);*/
	}
}
