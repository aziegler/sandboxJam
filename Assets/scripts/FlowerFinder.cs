using UnityEngine;
using System.Collections;

public class FlowerFinder : MonoBehaviour {

    public Transform SporeRoot;
    public FlowerRoot Flower;

    public bool Sterile
    {
        get { return _sterile; }
        set
        {
            _sterile = value;
            if (!value)
            {

                particle.GetComponent<ParticleSystem>().Play(true);
       
            }
            else
            {
                Destroy(particle);
            }
        }
    }

    public GameObject particle;
    private bool _sterile;


    // Use this for initialization
	void Start () 
    {
        Transform root = FindRoot(transform);
        FlowerRoot f = root.GetComponent<FlowerRoot>();
        Flower = f;
	    var findGameObjectsWithTag = GameObject.FindGameObjectsWithTag("Text");
	    foreach (var o in findGameObjectsWithTag)
	    {
	        o.GetComponent<MeshRenderer>().sortingLayerID = 4;
            o.GetComponent<MeshRenderer>().sortingLayerName = "Flowers";
	        o.GetComponent<MeshRenderer>().sortingOrder = 4;
	    }

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
