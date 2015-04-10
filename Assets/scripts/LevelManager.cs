using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Ground Ground;
    public GameObject[] FlowerPrefabs;
    public GameObject[] SeedPrefabs;

	// Use this for initialization
	void Start ()
    {
        int numberOfFlower = Random.Range(5, 8);

        for(int i = 0; i < numberOfFlower; i++)
        {
            GameObject go = GameObject.Instantiate(FlowerPrefabs[Random.Range(0, FlowerPrefabs.Length)]);

            Vector3 position = new Vector3(Random.Range(Ground.BorderLeft.position.x, Ground.BorderRight.position.x),
            Ground.BorderLeft.position.y,
            Ground.transform.position.z);

            go.GetComponent<Flower>().SeedPrefab = SeedPrefabs[Random.Range(0, SeedPrefabs.Length)];

            go.transform.position = position;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
