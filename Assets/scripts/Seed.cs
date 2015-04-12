using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{

    public Transform Flower;
    public GameObject Planet;
    public ParticleSystem particle;
    public bool IsCaptured = false;
	// Use this for initialization
    public SpriteRenderer[] Sprites;

	void Start ()
	{
        particle.GetComponent<Renderer>().sortingLayerName = "Flowers";
        particle.GetComponent<Renderer>().sortingOrder = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FadeAndKill()
    {
        foreach(SpriteRenderer sr in Sprites)
        {
            FadeObject fo = sr.gameObject.AddComponent<FadeObject>();
            fo.OnHide = Kill;
        }
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
 
}
