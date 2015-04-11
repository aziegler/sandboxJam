using UnityEngine;
using System.Collections;

public class Spore : MonoBehaviour {

    public int Level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
       /* Destroy(gameObject);*/
    }

    private bool exploded = false;

    void OnCollisionEnter2D(Collision2D col)
    {
       var spore = col.gameObject.GetComponent<Spore>();
       if (spore != null && spore.exploded)
        {
            GameManager.Instance.CreateSeed(this, spore);
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(gameObject);
        }

        var planet = col.gameObject.GetComponent<Planet>();
        if(null != planet)
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    public void Explode()
    {
        exploded = true;
    }


}
