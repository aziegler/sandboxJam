using UnityEngine;
using System.Collections;

public class flowerAnimateManager : MonoBehaviour {
	public 	 bool	explodeNow;
	public 	 int 	count;
	public	 Vector3 explosionPosition;

	// Use this for initialization
	void Start () {
		explodeNow = false;
		count = 0;	
	}

	public void InitExplosion(Vector3 explosionWorldPosition)
	{
		count = 2;
		explosionPosition = explosionWorldPosition;

	}

	// Update is called once per frame
	void FixedUpdate () {

		Debug.DrawLine(explosionPosition, explosionPosition + new Vector3 (0,1,0), Color.red);

		if (count>0) {
			explodeNow = true;
            print("update with explosion");
			count --;
			if (count < 0) {
				count = 0;
			}
		}
		else{
			explodeNow = false;
		}
	}
}
