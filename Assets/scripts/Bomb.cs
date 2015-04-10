using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public float Range;
    float TimeToExplose = 3f;

    float dateToExplose;
    bool exploded = false;
    Collider2D collider2d;

	// Use this for initialization
	void Start () 
    {
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;

        dateToExplose = Time.time + TimeToExplose;
	}
	
	// Update is called once per frame
	void Update () {

        if(!exploded && Time.time > dateToExplose)
        {
            exploded = true;
            Explode();
        }
	
	}

    void Explode ()
    {
        collider2d.enabled = true;

        GameObject.Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.velocity = new Vector2(0f, 6f);
    }
}
