using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Bomb;
    public Transform Laser;
    private Transform _laser;
    public Transform PlanetObject;
    public Transform Seed;
    public float nextSeed;

    public int currentMaxLevel = 0;

    public AudioSource firstSource;
    public AudioSource secondSource;
    public AudioSource thirdSource;

    public GameObject[] Flowers;
    public GameObject[] Seeds;
    private AudioSource _sourceToPlay;

    public bool IsLaserKillFlower;
    public bool IsNewFlowerReplaceOldOne;

    void Awake ()
    {
        Instance = this;
    }

    // Use this for initialization
	void Start ()
	{
	    GetNextSeedDate();
	    PlanetObject = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,5f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;

	 


	}

    private void GetNextSeedDate()
    {
        nextSeed = Time.time + Random.Range(4f, 5f);
    }

    public void SwitchSoundSource(int index)
    {
        switch (index)
        {
            case 1:
                _sourceToPlay = firstSource;
                break;
            case 2:
                _sourceToPlay = secondSource;
                break;
            case 3:
                _sourceToPlay = thirdSource;
                break;
        }
        StartCoroutine("FadeInOut", firstSource);
        StartCoroutine("FadeInOut", secondSource);
        StartCoroutine("FadeInOut", thirdSource);
    }

    // Update is called once per frame
	void Update () {
 	    if (Input.GetMouseButtonDown(0))
	    {
	        _laser.gameObject.GetComponent<Laser>().ShootRound();
	       
	    }
	    var F1 = Input.GetKeyDown(KeyCode.R);
	    var F2 = Input.GetKeyDown(KeyCode.T);
	    var F3 = Input.GetKeyDown(KeyCode.Y);
	    if (F1 || F2 || F3)
	    {
	        if (F1)
	            _sourceToPlay = firstSource;
	        if (F2)
	            _sourceToPlay = secondSource;
	        if (F3)
	            _sourceToPlay = thirdSource;

            print(_sourceToPlay);
	        StartCoroutine("FadeInOut", firstSource);
            StartCoroutine("FadeInOut", secondSource);
            StartCoroutine("FadeInOut", thirdSource);
	        
	    }

	    float horizontal = Input.GetAxis("Horizontal");
        PlanetObject.Rotate(new Vector3(0f, 0f, 90f * horizontal * Time.deltaTime));

	    if (Time.time > nextSeed)
	    {
	        var instantiate = (Transform) Instantiate(Seed, new Vector3(Random.Range(-10f,10f), 30f), Quaternion.identity);
	        instantiate.gameObject.AddComponent<OrbitMove>();
	        GetNextSeedDate();    
	    }
	  
	   
	}

    

    private IEnumerator  FadeInOut( AudioSource audioSource)
    {
        float time = 0f;
        var fadeTime = 1f;
        while (time < fadeTime)
        {
            time = time + 0.1f;
            var targetVolume = 0f;
            if(audioSource == _sourceToPlay)
                targetVolume = Mathf.Max(time/fadeTime,audioSource.volume);
            else
                targetVolume = Mathf.Min(1f - time/fadeTime, audioSource.volume);
            audioSource.volume = targetVolume;
            yield return new WaitForSeconds(0.1f);
         }

    
    }

    public void CreateSeed (Spore s1, Spore s2)
    {
        if (s1.Level == s2.Level && s1.Flower != s2.Flower && !s1.HasCollided && !s2.HasCollided)
        {
            s1.HasCollided = true;
            s2.HasCollided = true;
            int newLevel = s1.Level + 1 ;
            if(newLevel < Flowers.Length)
            {
                GameObject go = GameObject.Instantiate(Seeds[newLevel]);
                go.transform.position = s1.transform.position;
                var component = GameObject.FindGameObjectWithTag("SFXPlayer").GetComponent<FX>();
                component.PlaySound();
            }

            s1.Kill();
            s2.Kill();
           /*foreach (var seedSpawn in s1.Flower.SeedSpawns)
            {
                foreach (Transform child in seedSpawn.transform)
                {

                    GameObject.Destroy(child.gameObject);
                }
            }
            foreach (var seedSpawn in s2.Flower.SeedSpawns)
            {
                foreach (Transform child in seedSpawn.transform)
                {

                    GameObject.Destroy(child.gameObject);
                }
            }*/
        }
        else
        {
            Physics2D.IgnoreCollision(s1.GetComponent<Collider2D>(), s2.GetComponent<Collider2D>());
        }
    }
}