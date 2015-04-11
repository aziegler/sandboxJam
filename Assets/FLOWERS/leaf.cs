using UnityEngine;
using System.Collections;

public class leaf : MonoBehaviour {

	public float		angleVariation;
	public float 		angle;
	public float		animate;

	// Use this for initialization
	void Start () {

		//Make sure leaf starts in ungrown state
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//Grow Leaf
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one, 0.02f);

		//Animate Leaf
		animate += 0.004f;
		if(animate >1){
			animate -= 1;
		}
		
		transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));		
	}
}
