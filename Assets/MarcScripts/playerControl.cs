using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	public GameObject planet;
	public GameObject flower;
	public Rigidbody rb;
	

	// Use this for initialization
	void Start () 
	{
		rb = planet.GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Keyboard Control
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 force = new Vector3 (0, 0, 8);
			rb.AddTorque (force);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 force = new Vector3 (0, 0, -8);
			rb.AddTorque (force);
		}

		//mouse control
		Vector3 mousePositionNormalised = Input.mousePosition;
		mousePositionNormalised.x /= Screen.width;
		mousePositionNormalised.y /= Screen.height;

		if (mousePositionNormalised.x < 0.1f) {
			Vector3 force = new Vector3 (0, 0, -8);
			rb.AddTorque (force);
		}
		
		if (mousePositionNormalised.x > 0.9f) {
			Vector3 force = new Vector3 (0, 0, 8);
			rb.AddTorque (force);
		}

		//Plant flower
		if (Input.GetMouseButtonDown (0)) {
			Vector3 flowerPosition = new Vector3(0,3.4f,-1.50f);
			GameObject madeFlower = (GameObject) Instantiate(flower,flowerPosition,Quaternion.identity);
			madeFlower.transform.parent = planet.transform;
		}
	}
}
