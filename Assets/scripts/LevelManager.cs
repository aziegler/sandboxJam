using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Ground Ground;
    public GameObject[] FlowerPrefabs;
    public GameObject[] SporePrefabs;
    public int minNumberOfFlower;
    public int maxNumberOfFlower;

	// Use this for initialization
	void Start ()
    {
        int numberOfFlower = Random.Range(minNumberOfFlower, maxNumberOfFlower);

        for(int i = 0; i < numberOfFlower; i++)
        {
            GameObject go = GameObject.Instantiate(FlowerPrefabs[Random.Range(0, FlowerPrefabs.Length)]);

            Vector3 position = new Vector3(Random.Range(Ground.BorderLeft.position.x, Ground.BorderRight.position.x),
            Ground.BorderLeft.position.y,
            Ground.transform.position.z);

            go.GetComponent<FlowerRoot>().SporePrefab = SporePrefabs[Random.Range(0, SporePrefabs.Length)];

            go.transform.position = position;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
