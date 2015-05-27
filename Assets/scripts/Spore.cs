using UnityEngine;
using System.Collections;

public class Spore : MonoBehaviour {

    public int Level;
    SpriteRenderer sprite;
    TrailRenderer trail;

	public float BirthTime;

	void Awake ()
	{
		BirthTime = Time.time;
	}

	// Use this for initialization
	public void Init ()
	{
	    HasCollided = false;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        trail = GetComponent<TrailRenderer>();
        trail.sortingLayerName = "Flowers";
        trail.sortingOrder = 5;
        trail.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
       /* Destroy(gameObject);*/
    }

    private bool flying = false;

    public bool IsFlying
    {
        get { return flying; }
    }

    public FlowerRoot Flower { get; set; }
    public bool HasCollided { get; set; }
    public FlowerFinder FlowerHead { get; set; }

    void OnCollisionEnter2D(Collision2D col)
    {
       var spore = col.gameObject.GetComponent<Spore>();
       if (spore != null && spore.flying && this.flying)
        {
            
            GameManager.Instance.CreateSeed(this, spore);
        }

        var planet = col.gameObject.GetComponent<Planet>();
        if(null != planet && this.flying)
        {
            Kill();
        }
    }


    public void Explode()
    {
        flying = true;
        sprite.enabled = true;
        trail.enabled = true;
    }

    public void OnDestroy ()
    {
        if (null != Flower)
        {
            Flower.SporeDie();
        }
    }

    public void Kill ()
    {
		StartCoroutine ("Fade", 0f);
    }

	IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = sprite.color.a;
		float t = 0f;
		float startTime = Time.time;
		
		do {
			t = Mathf.Clamp ((Time.time - startTime) / Planet.Instance.SporeFadeDuration, 0f, 1f);
			Color c = sprite.color;
			c.a = Mathf.Lerp (startAlpha, targetAlpha, t);
			sprite.color = c;
			yield return null;
		} while(t<1f);

		GameObject.Destroy(this.gameObject);
	}
}
