using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	Camera _mainCamera;

	// Use this for initialization
	void Start () {
		_mainCamera = Camera.main;
		if(null!=_mainCamera)
			_mainCamera.enabled = false;
	}

    void OnMouseDown()
    {
		if(null != _mainCamera)
			_mainCamera.enabled = true;

		Object[] creditRoots = GameObject.FindGameObjectsWithTag ("CreditRoot");

		foreach(Object o in creditRoots)
		{
			GameObject.Destroy(o);
		}
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
