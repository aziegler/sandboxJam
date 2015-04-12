using UnityEngine;
using System.Collections;

public class FlowerRoot : MonoBehaviour
{

    public int Level;
    public int GrowthLevel = 1;

    public Transform[] SeedSpawns;
    public GameObject SporePrefab;
    private bool hasSpore;

    public bool HasSpore
    {
        get { return hasSpore; }
        set { hasSpore = value; }
    }

    public Voices Voice { get; set; }

    private int sporeCount = 0;

    private int SporeCount
    {
        get { return sporeCount; }
        set
        {
            if (value == 0 && sporeCount == 1)
            {
                Invoke("SpawnSpores", 0.7f);
            }
            sporeCount = value;
        }
    }

    // Use this for initialization
    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Planet.Instance.GetOrientedAngle(transform));
        Voice.Growth();
        HasSpore = true;
    }

    public void SpawnSpores()
    {
        if (Level == 3)
            return;

        HasSpore = true;
        foreach (Transform t in SeedSpawns)
        {
            GameObject go = GameObject.Instantiate(SporePrefab);
            go.transform.parent = t;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
            go.GetComponent<Spore>().Level = Level;
            go.GetComponent<Spore>().Flower = this;

            SporeCount++;
        }
    }

    // Update is called once per frame
    private void Update()
    {

        if (!HasSpore)
        {
            HasSpore = true;
            Invoke("SpawnSpores", 0.7f);
        }
        else
        {
            /*bool foundSittingSpore = false;
	        foreach (Transform t in SeedSpawns)
	        {
	            foreach (var componentsInChild in t.GetComponentsInChildren<Transform>())
	            {
	                var component = componentsInChild.GetComponent<Spore>();
	                if (component != null)
	                {
	                    if (!component.IsFlying)
	                        foundSittingSpore = true;
	                }
	            }
	        }
	        if (!foundSittingSpore)
	        {
                Invoke("SpawnSpores", 0.7f);
	        }*/
            if (!HasSpore)
            {
                HasSpore = true;
                //Invoke("SpawnSpores", 0.7f);
            }
        }


    }

    public void Kill()
    {
        Voice.Death();
        GameObject.Destroy(gameObject);
    }

    public void SporeDie()
    {
        SporeCount--;
    }
}
