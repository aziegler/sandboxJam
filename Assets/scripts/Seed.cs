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
        Destroy(gameObject);
    }

    private bool exploded = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        var seed = col.gameObject.GetComponent<Seed>();
        if (seed != null && seed.exploded)
        {


            Debug.Log(col.gameObject.name);
            var joint = gameObject.AddComponent<DistanceJoint2D>();
            joint.distance = 0.3f;
            joint.maxDistanceOnly = true;
            //joint.collideConnected = true;
            joint.connectedAnchor = new Vector2(0.5f, 0f);

            joint.connectedBody = col.rigidbody;
        }
    }


    public void Explode()
    {
        exploded = true;
    }
}
