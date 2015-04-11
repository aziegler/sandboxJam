using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
    public GameObject Planet;
    public ParticleSystem particle;

	// Use this for initialization
	void Start ()
	{
        particle.GetComponent<Renderer>().sortingLayerName = "Flowers";
        particle.GetComponent<Renderer>().sortingOrder = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
