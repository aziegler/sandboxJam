using UnityEngine;
using System.Collections;

public class FlowerFinder : MonoBehaviour {

    public Transform SporeRoot;
    public FlowerRoot Flower;

	// Use this for initialization
	void Start () 
    {
        Transform root = FindRoot(transform);
        FlowerRoot f = root.GetComponent<FlowerRoot>();
        Flower = f;

        /*if (null != SporeRoot)
        {
            f.SeedSpawns = new Transform[SporeRoot.childCount];
            for (int i = 0; i < SporeRoot.childCount; i++)
            {
                f.SeedSpawns[i] = SporeRoot.GetChild(i);
            }
            f.SpawnSpores();
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    Transform FindRoot(Transform t)
    {
        if(null == t.parent)
        {
            return t;
        }
        else if(t.tag == "Flower")
        {
            return t;
        }
        else
        {
            return FindRoot(t.parent);
        }
    }
}
