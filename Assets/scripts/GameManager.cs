using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Transform Bomb;
    public Transform Laser;
    private Transform _laser;
    public Transform Planet;

    // Use this for initialization
	void Start ()
	{
	    Planet = GameObject.FindGameObjectWithTag("Planet").transform;
	    _laser = (Transform)Instantiate(Laser, new Vector3(0f,5f), Quaternion.identity);
	    _laser.GetComponent<BoxCollider2D>().enabled = false;
	}

    // Update is called once per frame
	void Update () {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    _laser.position = new Vector3(mouseWorldPosition.x,mouseWorldPosition.y);
        Vector3 dir = _laser.transform.position - Planet.position;
	    float angle = (float) (Math.Atan(dir.x/dir.y)*Mathf.Rad2Deg);
        _laser.transform.eulerAngles = new Vector3(0f, 0f,180-angle);
	    if (Input.GetMouseButtonDown(0))
	    {
	        _laser.gameObject.GetComponent<Laser>().ShootRound();
	       
	    }
	}
}