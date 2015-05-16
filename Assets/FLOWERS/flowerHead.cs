using UnityEngine;
using System.Collections;

public class flowerHead : MonoBehaviour {

	//public float		angleVariation;
	public float 		angle;
	public float		animate;
	float				endscaleVariation;
  

	// Use this for initialization
	void Start () {

		//Make sure head starts in ungrown state
		transform.localScale = Vector3.zero;
		//endscaleVariation = Random.Range (0.8f, 1.4f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//Grow Head
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one, 0.06f);

		//Animate Head
		animate += 0.004f;
		if(animate >1){
			animate -= 1;
		}
		
		transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));		
	}
}
