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

        GameObject.Destroy(collider2d, 1f);

        StartCoroutine("ResetGame");
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
        if(this.transform.position.x < other.transform.position.x)
        {
            rb.AddForce(new Vector2(Random.Range(0.05f,0.15f), 1f) * Random.Range(9f,12f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(Random.Range(-0.15f,-0.05f), 1f) * Random.Range(9f,12f), ForceMode2D.Impulse);
        }
        
        Physics2D.IgnoreCollision(this.collider2d, other);
    }
}
