using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

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
       var seed = col.gameObject.GetComponent<Seed>();
       if (seed != null && seed.exploded)
        {
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(gameObject);
        }

        var planet = col.gameObject.GetComponent<Planet>();
        if(null != planet)
        {
            GameObject.Destroy(this.gameObject, 0.2f);
        }
    }


    public void Explode()
    {
        exploded = true;
    }


}
