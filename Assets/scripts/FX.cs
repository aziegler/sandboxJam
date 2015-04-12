using UnityEngine;
using System.Collections;

public class FX : MonoBehaviour {

    public AudioClip[] clips ;
    public AudioSource source;
	// Use this for initialization
	void Start () {
	
	}

    public void PlaySound()
    {
        if (source.isPlaying)
            return;
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
