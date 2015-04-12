using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour
{


    public Animator animator;
    public SoundCloud synchronisator;
	// Use this for initialization
    public void Play()
    {
        print("Tringgering play on "+animator.name+" at "+Time.time);
        animator.SetTrigger("Play");
    }

    public void Stop()
    {
        animator.SetTrigger("Stop");
    }

    public void Shoot()
    {
        synchronisator.Shoot();
    }
}
