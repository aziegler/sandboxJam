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

        GameObject.Destroy(collider2d, Time.fixedDeltaTime*2f);
        Destroy(gameObject,Time.fixedDeltaTime*2f);

      //  StartCoroutine("ResetGame");
    }

    IEnumerator ResetGame ()
    {
        yield return new WaitForSeconds(5f);
        Application.LoadLevel(0);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        var spore = other.GetComponent<Spore>();
        if (spore == null || spore.IsFlying)
            return;
        if (spore.Flower.HasSpore)
        {
            if(Random.Range(0,20)>15)
                spore.Flower.Voice.Touch();
        }
        spore.Explode();
        print("HasSpore : " + spore.Flower.HasSpore);
        spore.Flower.HasSpore = false;
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
       
        rb.isKinematic = false;

        var vector3 = other.transform.position - transform.position;

        var dir = new Vector2(Random.Range(vector3.x - 0.2f, vector3.x + 0.2f),
            Random.Range(vector3.y - 0.2f, vector3.y + 0.2f));

        dir = new Vector2(Math.Sign(vector3.x) * Random.Range(0.3f, 0.4f), 0.3f);

        dir.Normalize();
        var force = dir * 30f;
        
        rb.AddForce(force, ForceMode2D.Impulse);
        
        
        Physics2D.IgnoreCollision(this.collider2d, other);
        spore.gameObject.AddComponent<OrbitMove>();
       
    }
}
