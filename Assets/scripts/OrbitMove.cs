using UnityEngine;
using System.Collections;

public class OrbitMove : MonoBehaviour {

    Transform planet;
    Rigidbody2D rb;
    public float Force = .1f;

	// Use this for initialization
	void Start () 
    {      
        planet = GameObject.FindGameObjectWithTag("Planet").transform;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 direction = planet.position - transform.position;
        direction.Normalize();

        rb.velocity += (direction*Force);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //
    }
}
