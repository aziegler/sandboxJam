using System;
using UnityEngine;
using System.Collections;

public class SoundCloud : MonoBehaviour
{
    public AudioSource sound;
    public Transform laser;
    private float nextShoot;
    private float longInterval = 4.5f;
    private float shortInterval = 1.5f;
    private bool nextIsLong = false;
    private float audioTime = 0.0f;
    private float lastFrameAudioTime = -1.0f;
    private float animDelay = 0.25f;
    private float _nextLoad;
    private float _nextShootAnim;
    private bool loading = false;
    private bool shooting = false;
    private bool hasShot = false;

    // Use this for initialization
    void Start()
    {
        SetShootTime(longInterval - 0.75f);
    }

    private void SetShootTime(float shoot)
    {
        loading = false;
        shooting = false;
        hasShot = false;
        nextShoot = shoot;
        _nextLoad = shoot - 2.5f;
        _nextShootAnim = shoot - animDelay;
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

    public void ResyncSound(float timeFromMusiStart)
    {
        //print (audioTime * 80 / 60);
        audioTime = timeFromMusiStart;
    }

    void OnGUI()
    {
        GUILayout.Label(string.Format("Countdown : {0}", nextShoot - audioTime));
    }


    public void Shoot()
    {
        hasShot = true;
        laser.gameObject.GetComponent<Laser>().ShootRound();
       
        print ("Shooting "+audioTime);
        //print (nextShoot);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (audioTime < lastFrameAudioTime)
        {
            print("Looping sound");
            var shoot = nextShoot % 96.0f;
            SetShootTime(shoot);
        }

        lastFrameAudioTime = audioTime;
      
        if(audioTime >= _nextShootAnim && !shooting)
            PlayShootAnim();
        
            

        if (audioTime >= nextShoot && !hasShot)
            Shoot();

        /*float[] samples = new float[256];
        sound.GetOutputData(samples, 0);
        float average = 0;
        for (int count = 0; count < 255; count++)
        {
            average += samples[count];
        }
*/


//        float sphereScale = Mathf.Abs(average);
        /*if (sphereScale > 1) {
            laser.gameObject.GetComponent<Laser>().ShootRound();	*/
    }

    private void PlayShootAnim()
    {
        shooting = true;
        ShootAnim.Play();
    }

    public void RecomputeDelay()
    {
        animDelay = Math.Max(0.25f,audioTime - _nextShootAnim);
        if(!hasShot)
            Shoot();
        if (nextIsLong)
            SetShootTime(nextShoot + longInterval);            
        else
            SetShootTime(nextShoot + shortInterval);
        nextIsLong = !nextIsLong;
        ShootAnim.Stop();
        shooting = false;
    }
}


