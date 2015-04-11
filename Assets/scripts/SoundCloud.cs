using System;
using UnityEngine;
using System.Collections;

public class SoundCloud : MonoBehaviour {
	public AudioSource sound;
    public Transform laser;
    private float nextShoot;
    private float longInterval = 6f;
    private float shortInterval = 1.5f;
    private bool nextIsLong = false;

	// Use this for initialization
	void Start ()
	{
	    nextShoot = Time.time + longInterval;
	}

    void OnGUI()
    {
        GUILayout.Label(string.Format("Countdown : {0}", nextShoot - Time.time));
    }


    private void Shoot()
    {
         laser.gameObject.GetComponent<Laser>().ShootRound();
        if (nextIsLong)
            nextShoot = Time.time + longInterval;
        else
            nextShoot = Time.time + shortInterval;
        nextIsLong = !nextIsLong;
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
        if(Time.time > nextShoot)
            Shoot();
		float[] samples = new float[256];
		sound.GetOutputData (samples, 0);
		float average = 0;
		for (int count = 0; count < 255; count ++) {
			average += samples[count];
		}

     
		
		float sphereScale = Mathf.Abs (average);
		/*if (sphereScale > 1) {
		    laser.gameObject.GetComponent<Laser>().ShootRound();	*/
        }
		



	
	}


