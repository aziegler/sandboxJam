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
    public Transform FrontParallax;
    public float FrontParallaxCoeff;
    public Transform BackParallax;
    public float BackParallaxCoef;
    public Transform Sky;
    public float SkyCoeff;

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
    public int Countdown = 0;

    void Awake ()
    {

        Instance = this;
        Countdown = 0;
        _startTime = Time.time;
        _lastZoomDate = _startTime;
        GameManager.Instance.SwitchSoundSource(2);
    }

    // Use this for initialization
	void Start ()
	{
	    GetNextSeedDate();
	    PlanetObject = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,5f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;

	 


	}

    public float GetTime()
    {
        return Time.time - _startTime;
    }

    private void GetNextSeedDate()
    {
        nextSeed = Time.time + Random.Range(5f, 7f);
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
 	   

        if (GameOver && (Input.GetButtonDown("Fire1")))
        {
            Application.LoadLevel(1);
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

	    if (GameOver)
	        return;

	    float horizontal = Input.GetAxis("Horizontal");
	    var planetRotation = 90f * horizontal * Time.deltaTime;
	    PlanetObject.Rotate(new Vector3(0f, 0f, planetRotation));
        FrontParallax.Rotate(new Vector3(0f, 0f, planetRotation*FrontParallaxCoeff));
        BackParallax.Rotate(new Vector3(0f, 0f, planetRotation * BackParallaxCoef));
        Sky.Rotate(new Vector3(0f, 0f, planetRotation * SkyCoeff));

	    if (Time.time > nextSeed)
	    {
	        var instantiate = (Transform) Instantiate(Seed, new Vector3(Random.Range(-5f,5f), 30f), Quaternion.identity);
	        instantiate.gameObject.AddComponent<OrbitMove>();
	        instantiate.GetComponent<OrbitMove>().Force = 0.02f;
	        GetNextSeedDate();    
	    }
	    if (GetTime() > 150f)
	    {
            GameManager.Instance.SwitchSoundSource(3);
	    }

	    
	    Countdown = (int) (_maxUnzoom * _zoomDuration - GetTime());
	    if (Countdown <= 0)
	    {
	        ZoomOut();
	        GameOver = true;
	    }
        if (GetTime() > _lastZoomDate + _zoomDuration && !GameOver)
        {
            Unzoom(GetTime() - _lastZoomDate);
            _lastZoomDate = GetTime();
        }

	}

    public bool GameOver { get; set; }


    private static float _zoomDuration = 100f;
    private static float _lastZoomDate = 0f;
    private static int _maxUnzoom = 3;
    public float CurrentZoom = 0f;
    private float ShakeDuration = 0.3f;
    private float ShakeMagnitude = 0.1f;
    private float _startTime;


    private void ZoomOut()
    {

        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        var camera = mainCamera.GetComponent<Camera>();
        
        camera.orthographicSize = 20;
        mainCamera.transform.localPosition = new Vector3(0f, 0f, mainCamera.transform.localPosition.z);
    }

    private void Unzoom(float deltaTime) 
    {
        var zoom = ((_maxUnzoom * deltaTime) / _zoomDuration);
      
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        var camera = mainCamera.GetComponent<Camera>();
        CurrentZoom += zoom;
        camera.orthographicSize = camera.orthographicSize +zoom ;
        mainCamera.transform.localPosition = new Vector3(0f, mainCamera.transform.localPosition.y - zoom,mainCamera.transform.localPosition.z);
            
    }

    public void ShakeCam()
    {
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < ShakeDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / ShakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= ShakeMagnitude * damper;
            y *= ShakeMagnitude * damper;

            y += originalCamPos.y;

            Camera.main.transform.position = new Vector3(originalCamPos.x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
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
                var direction = go.transform.position - PlanetObject.position;
                direction.Normalize();
                go.GetComponent<Rigidbody2D>().velocity = direction*2f;
                go.GetComponent<Seed>().firstParent = s1.FlowerHead;
                go.GetComponent<Seed>().secondParent = s2.FlowerHead;
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