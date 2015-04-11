using System;
using UnityEngine;
using System.Collections;

public class SoundCloud : MonoBehaviour {
	public AudioSource sound;
    public Transform laser;


	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	    if (laser == null)
	    {
	        try
	        {
	            laser = GameObject.FindGameObjectWithTag("Laser").transform;
	        }
	        catch (Exception e)
	        {
	            
	        }
	    }
		float[] samples = new float[256];
		sound.GetOutputData (samples, 0);
		float average = 0;
		for (int count = 0; count < 255; count ++) {
			average += samples[count];
		}

		
		print("Average reading: "+average);

		float sphereScale = Mathf.Abs (average);
		if (sphereScale > 1) {
		    laser.gameObject.GetComponent<Laser>().ShootRound();	
        }
		



	
	}
}

