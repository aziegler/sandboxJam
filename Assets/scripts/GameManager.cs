﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public const string HIGHSCORE_KEY = "pollinator.highscore";
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
    public UnityEngine.UI.Text ScoreGUI;
    public UnityEngine.UI.Text FinalScoreGUI;
	public Text HighScore;

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

    private int _score = 0;
    public int Countdown = 0;
	bool highscoreset = false;


    public Boolean TutoFirstStep = true;
    public Boolean TutoSecondStep = false;
    public Boolean TutoThirdStep = false;
 
	public Text timerUIText;
	public Image timerUIImage;

	public Image TutoFour;

    void Awake ()
    {

        Instance = this;
        Countdown = 0;
        _startTime = Time.time;
        _lastZoomDate = GetTime();
        GameManager.Instance.SwitchSoundSource(2);
    }

    // Use this for initialization
	void Start ()
	{
	    GetNextSeedDate();
	    PlanetObject = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,5f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;

	    FinalScoreGUI.enabled = false;
		HighScore.enabled = false;
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
		/*
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
		*/
	   

	    float horizontal = Input.GetAxis("Horizontal");
	    if (horizontal > 0f && TutoFirstStep)
	    {
	        var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("TutoOne");
	        foreach (var o in findGameObjectsWithTag)
	        {
	            Destroy(o);
	        }
	        TutoFirstStep = false;
	    }
	    var planetRotation = 90f * horizontal * Time.deltaTime;
	    PlanetObject.Rotate(new Vector3(0f, 0f, planetRotation));
        FrontParallax.Rotate(new Vector3(0f, 0f, planetRotation*FrontParallaxCoeff));
        BackParallax.Rotate(new Vector3(0f, 0f, planetRotation * BackParallaxCoef));
        Sky.Rotate(new Vector3(0f, 0f, planetRotation * SkyCoeff));


	    if (!GameOver && Time.time > nextSeed)
	    {
	        var instantiate = (Transform) Instantiate(Seed, new Vector3(Random.Range(-5f,5f), 30f), Quaternion.identity);
	        instantiate.gameObject.AddComponent<OrbitMove>();
	        instantiate.GetComponent<OrbitMove>().Force = 0.02f;
	        GetNextSeedDate();    
	    }

		/// TODO : Launch sound 3 on the second Unzoom call. Better than call this one each frame.
	    if (!GameOver && GetTime() >= 200f)
	    {
            GameManager.Instance.SwitchSoundSource(3);
	    }
	    
        if(!GameOver)
    	    Countdown = (int) ((_maxUnzoom * _zoomDuration) - GetTime());

	    if (Countdown <= 0)
	    {
	        ZoomOut();
	        GameOver = true;
            SwitchSoundSource(1);
            PlanetObject.GetComponent<Planet>().ResetSlots();
            FinalScoreGUI.enabled = true;
	        FinalScoreGUI.text = Score.ToString();
	        ScoreGUI.fontSize = 60;
            ScoreGUI.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            var soundCloud = GameObject.FindGameObjectWithTag("SoundSync").GetComponent<SoundCloud>();
	        soundCloud.ShootAnim.Stop();
	        soundCloud.LoadAnim.Stop();
	        ScoreGUI.enabled = false;

			int lastHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY,0);
			bool newHighScore = false;
			if(Score > lastHighScore)
			{
				lastHighScore = Score;
				newHighScore = true;
				PlayerPrefs.SetInt(HIGHSCORE_KEY,Score);
			}

			if(!highscoreset)
			{
				highscoreset = true;
				HighScore.enabled = true;
				HighScore.text = "highscore: " + lastHighScore.ToString();

				if(newHighScore)
				{
					//Show something special ?
					HighScore.text = "new " + HighScore.text;
				}
			}

			timerUIImage.enabled = false;
			timerUIText.enabled = false;
	    }
        if (GetTime() > ((_lastZoomDate) + _zoomDuration) && !GameOver)
        {
            Unzoom();
            _lastZoomDate = GetTime();
        }

		// SET UI

		float secondTimer = Mathf.Clamp(300f - GetTime (),0f,300f);
		float timerFillAmout = secondTimer / 300f;
		timerUIImage.fillAmount = timerFillAmout;
		timerUIText.text = ((int)(secondTimer)).ToString ();
	}

    public bool GameOver { get; set; }

    public int Score
    {
        get { return _score; }
        set { _score = value;
            ScoreGUI.text = Score.ToString();
            FinalScoreGUI.text = Score.ToString();
        }
    }

    public float FirstZoomValue = 1.7f;
    public float SecondZoomValue = 1.3f;


    private static float _zoomDuration = 100;
    private static float _lastZoomDate = 0f;
    private static int _maxUnzoom = 3;
    public float CurrentZoom = 0f;
    private float ShakeDuration = 0.3f;
    private float ShakeMagnitude = 0.1f;
    private float _startTime;
    private float UnZoomDuration =0.5f;
    private float _zoomStep;
    private float _moveStep;


    private void ZoomOut()
    {

        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        var camera = mainCamera.GetComponent<Camera>();

        _moveStep = Camera.main.transform.localPosition.y;
        _zoomStep = 26 - Camera.main.orthographicSize;
        StartCoroutine("ZoomOutRoutine");
    }

    IEnumerator ZoomOutRoutine()
    {

        float elapsed = 0.0f;


        while (elapsed < 2 * UnZoomDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = Time.deltaTime / UnZoomDuration;

            var currentZoom = _zoomStep * percentComplete;
            CurrentZoom += currentZoom;
            Camera.main.orthographicSize += _zoomStep * percentComplete;
            Camera.main.transform.localPosition = new Vector3(0f, Camera.main.transform.localPosition.y - _moveStep * percentComplete, Camera.main.transform.localPosition.z);



            yield return null;
        }

    }

    private void Unzoom()
    {
        _zoomStep = 0f;
        if (CurrentZoom == 0f)
            _zoomStep = FirstZoomValue;
        else
            _zoomStep = SecondZoomValue;
        StartCoroutine("UnzoomRoutine");

    }

    IEnumerator UnzoomRoutine()
    {

        float elapsed = 0.0f;


        while (elapsed < UnZoomDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = Time.deltaTime / UnZoomDuration;

            var currentZoom = _zoomStep*percentComplete;
            CurrentZoom += currentZoom;
            Camera.main.orthographicSize += currentZoom;
            Camera.main.transform.localPosition = new Vector3(0f, Camera.main.transform.localPosition.y - currentZoom, Camera.main.transform.localPosition.z);
   
            

            yield return null;
        }

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
       /* GUI.contentColor = Color.black;
        GUI.Label(new Rect(1200,50,100,20),string.Format("Score : {0}", Score));
        GUI.Label(new Rect(1200, 100, 100, 20), string.Format("Temps : {0}", Countdown));*/
    }

    

    private IEnumerator  FadeInOut( AudioSource audioSource)
    {
        float time = 0f;
        var fadeTime = 0.5f;
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
            //print("Level created = " + s1.Level);
            if (s1.Level == 0 && TutoSecondStep)
            {
                TutoSecondStep = false;
                var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("TutoTwo");
                foreach (var o in findGameObjectsWithTag)
                {
					Image image = o.GetComponent<Image>();
					if(image==null)
					{
						Destroy(o);
					}
					else
					{
						image.gameObject.SendMessage(FadeTutorial.DESACTIVATE_TUTORIAL);
					}                   
                }
            }
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
                go.GetComponent<Seed>().AnimatCircle();
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

    public void FlowerCreated(int level)
    {
        currentFlowerCount ++;
        //print("Flower Count "+currentFlowerCount);
        if (currentFlowerCount == 2)
        {
            TutoSecondStep = true;
            var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("TutoTwo");
            foreach (var o in findGameObjectsWithTag)
            {
				SpriteRenderer sr = o.GetComponent<SpriteRenderer>();
				if (null!=sr)
					sr.enabled = true;

				Image image = o.GetComponent<Image>();
				if (null!=image)
				{
					Color c = image.color;
					c.a = 0f;
					image.color = c;

					image.enabled = true;

					image.gameObject.SendMessage(FadeTutorial.ACTIVTE_TUTORIAL);
				}
            }
        }
        if (level == 1 && !TutoThirdStep)
        {
            TutoThirdStep = true;
            var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("TutoThree");
            foreach (var o in findGameObjectsWithTag)
            {
				SpriteRenderer sprite = o.GetComponent<SpriteRenderer>();

				Color color = sprite.color;
				color.a = 0f;
				sprite.color = color;

				sprite.enabled = true;

				o.SendMessage(FadeSprite.ACTIVATE_SPRITE);
            }
            Invoke("EndThirdTuto",Planet.Instance.TutoSequenceDuration);
        }
    }

    public void EndThirdTuto()
    {
        var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("TutoThree");
        foreach (var o in findGameObjectsWithTag)
        {
            //Destroy(o);

			SpriteRenderer sprite = o .GetComponent<SpriteRenderer>();
			sprite.enabled = true;
			o.SendMessage(FadeSprite.DESACTIVATE_SPRITE);
        }

		Invoke ("ShowTutoFour", 5f);
    }

	public void ShowTutoFour()
	{
		Color c = TutoFour.color;
		c.a = 0f;
		TutoFour.color = c;
		TutoFour.enabled = true;

		TutoFour.gameObject.SendMessage(FadeTutorial.ACTIVTE_TUTORIAL);
		Invoke ("HideTutoFour", 10f);
	}

	public void HideTutoFour ()
	{
		TutoFour.gameObject.SendMessage (FadeTutorial.DESACTIVATE_TUTORIAL);
	}
}