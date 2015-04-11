using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Bomb;
    public Transform Laser;
    private Transform _laser;
    public Transform PlanetObject;
    public Transform Seed;
    public float nextSeed;

    void Awake ()
    {
        Instance = this;
    }

    // Use this for initialization
	void Start ()
	{
	    GetNextSeedDate();
	    PlanetObject = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,30f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;

        
	}

    private void GetNextSeedDate()
    {
        nextSeed = Time.time + Random.Range(1f, 2f);
    }

    // Update is called once per frame
	void Update () {
 	    if (Input.GetMouseButtonDown(0))
	    {
	        _laser.gameObject.GetComponent<Laser>().ShootRound();
	       
	    }
        float horizontal = Input.GetAxis("Horizontal");
        PlanetObject.Rotate(new Vector3(0f, 0f, 90f * horizontal * Time.deltaTime));

	    if (Time.time > nextSeed)
	    {
	        var instantiate = (Transform) Instantiate(Seed, new Vector3(Random.Range(-10f,10f), 30f), Quaternion.identity);
	        instantiate.gameObject.AddComponent<OrbitMove>();
	        GetNextSeedDate();    
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