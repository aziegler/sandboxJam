using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour {

    public float Range;
    float TimeToExplose = 0.1f;

    float dateToExplose;
    bool exploded = false;
    Collider2D collider2d;

	// Use this for initialization
	public void Init () 
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

        GameObject.Destroy(collider2d, 1f);
        Destroy(gameObject,1f);

      //  StartCoroutine("ResetGame");
    }

    IEnumerator ResetGame ()
    {
        yield return new WaitForSeconds(5f);
        Application.LoadLevel(0);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
       
        rb.isKinematic = false;

        var vector3 = other.transform.position - transform.position;

        var dir = new Vector2(Random.Range(vector3.x - 0.2f, vector3.x + 0.2f),
            Random.Range(vector3.y - 0.2f, vector3.y + 0.2f));
        dir.Normalize();
        var force = dir * 8f;
        
        rb.AddForce(force, ForceMode2D.Impulse);
        
        
        Physics2D.IgnoreCollision(this.collider2d, other);
        var seed = other.GetComponent<Seed>();
        seed.gameObject.AddComponent<OrbitMove>();
        seed.Explode();
    }
}
