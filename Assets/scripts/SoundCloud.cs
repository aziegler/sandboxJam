using System;
using UnityEngine;
using System.Collections;

public class SoundCloud : MonoBehaviour {
	public AudioSource sound;
    public Transform laser;
    private float nextShoot;
    private float nextLoad;
    private float nextShootAnim;
    private float stopAll;
    private float longInterval = 4.5f;
    private float shortInterval = 1.5f;
    private bool nextIsLong = false;

    private bool loading = false;
    private bool shooting = false;

	// Use this for initialization
	void Start ()
	{
	    var shoot = Time.time + 3.75f;
	    SetShootTime(shoot);
	}

    private void SetShootTime(float shoot)
    {
        loading = false;
        print("Shooting is false");
        shooting = false;
        nextShoot = shoot;
        nextLoad = shoot - 2.5f;
        nextShootAnim = shoot - 1f;
       // stopAll = shoot + 0.3f;
        print("Shoot time : " + nextShoot + " shootA " + nextShootAnim + " nextLoad " + nextLoad);
    }

    private void LaunchShootAnim()
    {
        print("Load shoot anim time " + Time.time);
        print("Shooting is true");
        shooting = true;
        LoadAnim.Stop();       
        ShootAnim.Play();
       
    }

    private void LaunchLoadAnim()
    {
        ShootAnim.Stop();
        LoadAnim.Play();
        loading = true;
        
    }

    public void ResyncSound(float timeFromMusiStart)
    {
        if (timeFromMusiStart > 3.75f)
            return;
        var shoot = Time.time + 3.75f - timeFromMusiStart;
        SetShootTime(shoot);
        nextIsLong = false;

    }

    void OnGUI()
    {
        GUILayout.Label(string.Format("Countdown : {0}", nextShoot - Time.time));
    }


    public void Shoot()
    {
        print("Shoot time " + Time.time);
         laser.gameObject.GetComponent<Laser>().ShootRound();
        if (nextIsLong)
            SetShootTime(nextShoot + longInterval);
        else
            SetShootTime(nextShoot + shortInterval);
        nextIsLong = !nextIsLong;
        ShootAnim.Stop();
    }

    private PlayAnimation ShootAnim
    {
        get
        {
            var playAnimation = GameObject.FindGameObjectWithTag("Shooting").GetComponent<PlayAnimation>();
            playAnimation.synchronisator = this;
            return playAnimation;
        }
    }

    private PlayAnimation LoadAnim
    {
        get
        {
            var playAnimation = GameObject.FindGameObjectWithTag("Loading").GetComponent<PlayAnimation>();
            playAnimation.synchronisator = this;
            return playAnimation;
        }
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
        if (Time.time > nextShootAnim && !shooting)
            LaunchShootAnim();
      
       /* if(Time.time > nextLoad && !loading)
            LaunchLoadAnim();*/
        

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

    private void StopAll()
    {
        //print("StopAll");
        loading = false;
        print("Shooting is false");
        shooting = false;
        LoadAnim.Stop();
        ShootAnim.Stop();
    }
}


