using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
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
		StartCoroutine ("Fade", 0f);
    }

	public void StopFade()
	{
		StopCoroutine ("Fade");
	}
	IEnumerator Fade(float targetAlpha)
	{
		yield return new WaitForSeconds (0.8f);

		float startAlpha = Sprites[0].color.a;
		float t = 0f;
		float startTime = Time.time;
		
		do {
			t = Mathf.Clamp ((Time.time - startTime) / Planet.Instance.SporeFadeDuration, 0f, 1f);

			ApplyAlphaOnSprite(Sprites[0], startAlpha, targetAlpha, t);
			ApplyAlphaOnSprite(Sprites[1], startAlpha, targetAlpha, t);

			yield return null;
		} while(t<1f);
		
		GameObject.Destroy(this.gameObject);
	}
	
	void ApplyAlphaOnSprite(SpriteRenderer sprite, float startAlpha, float targetAlpha, float  t)
	{
		Color c = sprite.color;
		c.a = Mathf.Lerp (startAlpha, targetAlpha, t);
		sprite.color = c;
	}
}
