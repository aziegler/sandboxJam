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
    public int currentFlowerCount = 0;

    public AudioSource firstSource;
    public AudioSource secondSource;
    public AudioSource thirdSource;

    public GameObject[] Flowers;
    public GameObject[] Seeds;
    private AudioSource _sourceToPlay;

    public bool IsLaserKillFlower;
    public bool IsNewFlowerReplaceOldOne;

    public int Score = 0;
    public int Countdown = 180;

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
	    if (Time.time > 90f)
	    {
            GameManager.Instance.SwitchSoundSource(3);
	    }

	    if (Time.time > _lastZoomDate + 60f)
	    {
	        Unzoom(Time.time - _lastZoomDate);
	        _lastZoomDate = Time.time;
	    }
	    Countdown = (int) (180 - Time.time);

	}

    private static float _lastZoomDate = 0f;
    private static int _maxUnzoom = 3;
    private static int _unzoomDuration = 120;
    public float CurrentZoom = 0f;

    private void Unzoom(float deltaTime) 
    {
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        var camera = mainCamera.GetComponent<Camera>();
        var zoom = ((_maxUnzoom*deltaTime)/_unzoomDuration);
        CurrentZoom += zoom;
        camera.orthographicSize = camera.orthographicSize +zoom ;
        mainCamera.transform.localPosition = new Vector3(0f, mainCamera.transform.localPosition.y - zoom,mainCamera.transform.localPosition.z);
            
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(1200,50,100,20),string.Format("Score : {0}", Score));
        GUI.Label(new Rect(1200, 100, 100, 20), string.Format("Temps : {0}", Countdown));
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
                var component = GameObject.FindGameObjectWithTag("SFXPlayerPollen").GetComponent<FX>();
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