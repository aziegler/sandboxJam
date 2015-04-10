using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public Transform[] SeedSpawns;
    public GameObject SeedPrefab;

	// Use this for initialization
	void Start () {
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
