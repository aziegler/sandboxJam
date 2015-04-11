using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) 
    {
         if (coll.gameObject.CompareTag("Planet"))
        {
            print("collision " + coll.contacts[0]);

            Instantiate(Flower, coll.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
