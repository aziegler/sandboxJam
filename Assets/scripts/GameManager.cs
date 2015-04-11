using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Bomb;
    public Transform Laser;
    private Transform _laser;
    public Transform PlanetObject;
    public Transform Seed;

    void Awake ()
    {
        Instance = this;
    }

    // Use this for initialization
	void Start ()
	{
	    PlanetObject = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,30f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;

        
	}

    // Update is called once per frame
	void Update () {
 	    if (Input.GetMouseButtonDown(0))
	    {
	        _laser.gameObject.GetComponent<Laser>().ShootRound();
	       
	    }
        float horizontal = Input.GetAxis("Horizontal");
        PlanetObject.Rotate(new Vector3(0f, 0f, 90f * horizontal * Time.deltaTime));

	    var random = new Random();
	    var nextDouble = random.NextDouble();
       if (nextDouble > 0.99f)
	    {
	        Instantiate(Seed, new Vector3(0f, 30f), Quaternion.identity);
	    }
	}

    public void CreateSeed (Spore s1, Spore s2)
    {
        GameObject go = GameObject.Instantiate(Seed.gameObject);
        go.transform.position = s1.transform.position;

        if (s1.Level == s2.Level)
        {
           
        }
    }
}