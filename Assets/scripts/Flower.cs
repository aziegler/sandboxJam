using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public Transform[] SeedSpawns;
    public GameObject SporePrefab;

	// Use this for initialization
	void Start ()
	{
        transform.eulerAngles = new Vector3(0f, 0f, Planet.Instance.GetOrientedAngle(transform));

        foreach(Transform t in SeedSpawns)
        {
            GameObject go = GameObject.Instantiate(SporePrefab);
            go.transform.parent = t;
            go.transform.localPosition = Vector3.zero;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
