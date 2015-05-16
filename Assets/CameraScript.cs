using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public Animator cameraAnim;
	// Use this for initialization
	void Start () {
        	
	}

    void Awake()
    {
        print("Already done : " + CreditButton.wentToCredit);
        if(CreditButton.wentToCredit)
            GoToAnimEnd();
    }

    void GoToAnimEnd()
    {
        print("Setting camera");
        cameraAnim.Play("");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
