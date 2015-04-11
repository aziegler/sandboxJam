using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
    public GameObject Planet;
	// Use this for initialization
	void Start ()
	{
	    Planet = GameObject.FindGameObjectWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) 
    {
         if (coll.gameObject.CompareTag("Planet"))
        {
            print("collision " + coll.contacts[0]);

            var instantiate = (GameObject) Instantiate(Flower, coll.contacts[0].point, Quaternion.identity);
            instantiate.transform.SetParent(Planet.transform);
            Destroy(gameObject);
        }
    }
}
