using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public int Level;
    public Transform[] SeedSpawns;
    public GameObject SporePrefab;
    public bool HasSpore { get; set; }

    // Use this for initialization
	void Start ()
	{
	    transform.eulerAngles = new Vector3(0f, 0f, Planet.Instance.GetOrientedAngle(transform));

	    SpawnSpores();
	}

    private void SpawnSpores()
    {
        HasSpore = true;
        foreach (Transform t in SeedSpawns)
        {
            GameObject go = GameObject.Instantiate(SporePrefab);
            go.transform.parent = t;
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<Spore>().Level = Level;
            go.GetComponent<Spore>().Flower = this;
        }
    }

    // Update is called once per frame
	void Update () {
	    if (!HasSpore)
	    {
	        HasSpore = true;
            Invoke("SpawnSpores",0.7f);
	    }
	}
}
