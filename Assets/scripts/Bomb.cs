using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;
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
        var head = other.GetComponent<FlowerFinder>();
        if (head == null || head.Sterile)
            return;
        var spore = head.Flower.SpawnSpore(head,head.transform);
        if (spore == null)
            return;
        if (Random.Range(0, 20) > 15)
            head.Flower.Voice.Touch();
        
        spore.GetComponent<Spore>().Explode();
       // spore.Flower.HasSpore = false;
        Rigidbody2D rb = spore.GetComponent<Rigidbody2D>();
       
        rb.isKinematic = false;

        var vector3 = other.transform.position - transform.position;

        
        var dir = new Vector2(Math.Sign(vector3.x) * Random.Range(0.3f, 0.7f), 0.7f);

        dir.Normalize();
        var force = dir * 30f;
        
        rb.AddForce(force, ForceMode2D.Impulse);
        
        
        Physics2D.IgnoreCollision(this.collider2d, other);
        spore.AddComponent<OrbitMove>();

    }
}
