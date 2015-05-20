using UnityEngine;
using System.Collections;

public class TitleMusic : MonoBehaviour {
	static TitleMusic _instance;

	public AudioSource introMusic;
	public AudioSource loopMusic;

	public float delayToFade;

	void Awake ()
	{
		if(null == _instance)
		{
			_instance = this;
			DontDestroyOnLoad (gameObject);		
		}
		else if(_instance.gameObject != gameObject)
		{
			GameObject.Destroy(gameObject);
		}
	}

	void OnLevelWasLoaded(int levelid)
	{
		if(levelid == 1)
		{
			StartCoroutine("FadeOut", introMusic);
			StartCoroutine("FadeOut", loopMusic);
			GameObject.Destroy(gameObject, (delayToFade+0.1f));
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator  FadeOut( AudioSource audioSource)
	{
		float time = 0f;
		var fadeTime = delayToFade;
		while (time < fadeTime)
		{
			time = time + 0.1f;
			var targetVolume = 0f;
			targetVolume = Mathf.Min(1f - time/fadeTime, audioSource.volume);
			audioSource.volume = targetVolume;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
