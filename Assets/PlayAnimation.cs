using System;
using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour
{


    public Animator animator;
    public Animation test;
    public SoundCloud synchronisator;
    public Boolean waitingShoot = false;
	// Use this for initialization
    public void Play()
    {
        waitingShoot = true;
        print("Tringgering play on "+animator.name+" at "+Time.time);
        animator.SetTrigger("Play");
        //test.Play();       
    }

    public void Stop()
    {
        animator.SetTrigger("Stop");
      //  test.Stop();
    }

    public void Shoot()
    {
        if (!waitingShoot)
            return;        

        synchronisator.RecomputeDelay();
    }
}
