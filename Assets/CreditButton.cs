using UnityEngine;
using System.Collections;

public class CreditButton : MonoBehaviour {
    public static bool wentToCredit;

    // Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnMouseDown()
    {
        wentToCredit = true;
        print("click start");
        Application.LoadLevel(2);
    }
}
