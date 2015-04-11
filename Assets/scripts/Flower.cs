using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public Transform[] SeedSpawns;
    public GameObject SeedPrefab;
    public Transform Planet;

	// Use this for initialization
	void Start ()
	{
	    Planet = GameObject.FindGameObjectWithTag("Planet").transform;
        Vector3 dir = Planet.position - transform.position;
	    var angle = Vector3.Angle( transform.up,dir);
        transform.eulerAngles = new Vector3(0f,0f,angle + 180);
        foreach(Transform t in SeedSpawns)
        {
            GameObject go = GameObject.Instantiate(SeedPrefab);
            go.transform.parent = t;
            go.transform.localPosition = Vector3.zero;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
