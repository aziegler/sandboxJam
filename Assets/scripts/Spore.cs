using UnityEngine;
using System.Collections;

public class Spore : MonoBehaviour {

    public int Level;

	// Use this for initialization
	void Start ()
	{
	    HasCollided = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
       /* Destroy(gameObject);*/
    }

    private bool flying = false;

    public bool IsFlying { get; set; }
    public Flower Flower { get; set; }
    public bool HasCollided { get; set; }

    void OnCollisionEnter2D(Collision2D col)
    {
       var spore = col.gameObject.GetComponent<Spore>();
       if (spore != null && spore.flying && this.flying)
        {
            
            GameManager.Instance.CreateSeed(this, spore);
        }

        var planet = col.gameObject.GetComponent<Planet>();
        if(null != planet)
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    public void Explode()
    {
        flying = true;
    }


}
