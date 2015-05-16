using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        print("click start");
        Application.LoadLevel(1);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
