using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
    public GameObject Planet;
    public ParticleSystem particle;
    public bool IsCaptured = false;
    public FlowerFinder firstParent;
    public FlowerFinder secondParent;
	// Use this for initialization
    public SpriteRenderer[] Sprites;
    public SpriteRenderer SeedCircle;

	void Start ()
	{
        particle.GetComponent<Renderer>().sortingLayerName = "Flowers";
        particle.GetComponent<Renderer>().sortingOrder = 10;
	   
	}

    public void AnimatCircle()
    {
        StartCoroutine("ScaleDownCircle");
    }
    IEnumerator ScaleDownCircle()
    {

        float elapsed = 0.0f;


        var duration = 0.7f;
        while (elapsed < duration)
        {


            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;

            float scale = (1 - percentComplete)*4f;

            SeedCircle.transform.localScale = new Vector3(scale,scale,scale);


            yield return null;
        }

        Destroy(SeedCircle);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FadeAndKill()
    {
        foreach(SpriteRenderer sr in Sprites)
        {
            FadeObject fo = sr.gameObject.AddComponent<FadeObject>();
            fo.OnHide = Kill;
        }
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
 
}
