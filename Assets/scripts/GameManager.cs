using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Transform Bomb;
    public Transform Laser;
    private Transform _laser;

    // Use this for initialization
	void Start ()
	{
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,5f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;
	}

    // Update is called once per frame
	void Update () {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    _laser.position = new Vector3(mouseWorldPosition.x,5f);
	    if (Input.GetMouseButtonDown(0))
	    {
	        _laser.gameObject.GetComponent<Laser>().Shoot();
	       
	    }
	}
}