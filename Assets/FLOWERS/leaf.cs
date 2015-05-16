using UnityEngine;
using System.Collections;

public class leaf : MonoBehaviour {

	public 	float		angleVariation;
	public 	float 		angle;
	public 	float		animate;
	public 	GameObject	root;
			float 		plosion	;

	// Use this for initialization
	void Start () {

		//Make sure leaf starts in ungrown state
		transform.localScale = Vector3.zero;

		root = transform.parent.GetComponent<stem> ().root;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//Grow Leaf
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one, 0.02f);

		//Animate Leaf
		if(root != null)
		{
		    var flowerLevel = root.GetComponent<flowerLevel>();
		    if (flowerLevel != null && flowerLevel.plosion){
				plosion = 2;
			}
		}

	    //Explosion Animation
		if(plosion >0 && plosion <= 2){
			
			if(plosion >=1 ){
				//In Explosion animation
				float plosionSmoothed = Mathf.SmoothStep(0,1,2-plosion);
				animate += 0.004f + (plosionSmoothed/10);
				Quaternion targetRotation = Quaternion.Euler(new Vector3(0,0,(Mathf.Sin (animate * (Mathf.PI *2)) * (5 +(plosionSmoothed*5)))));
				transform.localRotation = Quaternion.Lerp (transform.localRotation ,targetRotation,plosionSmoothed);
				plosion -= 0.3f;
			}
			if(plosion <1){
				//Out Explosion Animation
				float plosionSmoothed = Mathf.SmoothStep(0,1,1-plosion);
				animate += 0.004f + ((1-plosionSmoothed)/10);
				Quaternion targetAngle = Quaternion.Euler(new Vector3(0,0,Mathf.Sin (animate * (Mathf.PI *2)) * (5 + ((1-plosionSmoothed)*5))));
				transform.localRotation = Quaternion.Lerp (targetAngle,Quaternion.Euler(new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5))),plosionSmoothed);
				plosion -= 0.008f;if(plosion <0){plosion = 0;}
			}
			
		} else {
			
			//Swaying normally
			animate += 0.004f;if(animate >1){animate -= 1;}
			transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));
		}

		//animate += 0.004f;
		//if(animate >1){
		//	animate -= 1;
		//}
		
		//transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));		
	}
}
