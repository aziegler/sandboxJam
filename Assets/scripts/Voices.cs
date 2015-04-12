using UnityEngine;
using System.Collections;

public class Voices : MonoBehaviour
{

    public AudioClip[] births;
    public AudioClip[] touches;
    public AudioClip[] deaths;
    public AudioSource source;

	// Use this for initialization
	void Start ()
	{
	    

	}

    public void Death()
    {
        RandomSound(deaths);
    }

    public void Growth()
    {
        RandomSound(births);
    }

    public void Touch()
    {
        RandomSound(touches);
    }

    private void RandomSound(AudioClip[] clips)
    {
        //Source can ben null if flower of the spore is destroy
        if (null == source) { return; }

        if (source.isPlaying) return;
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
}
