using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        print("click start");
        Application.LoadLevel(0);
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
