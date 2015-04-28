using UnityEngine;
using System.Collections;

public class flowerLevel : MonoBehaviour {
	public int 			type;
	public int 			level;
	public int 			RandomSeed;
	public GameObject 	stem;
	public bool 		plosion;
	public GameObject 	flowerAnimationManager;
	public Vector3		explosionPosition;
	public Quaternion 	explosionRotation;

	// Use this for initialization
	void Start () {
		if (stem != null) {

			//Used for animating plants for explosion
			plosion = false;

			flowerAnimationManager = GameObject.Find ("flowerAnimationManager");

			//Flower One
			if(type == 0){

				//Level 1
				if(level == 0){
					stem.GetComponent<stem>().nodeNumber = 2;transform.localScale = Vector3.one * Random.Range(0.5f,1.0f);
				}

				if(level == 1){
					stem.GetComponent<stem>().nodeNumber = 1;transform.localScale = Vector3.one * Random.Range(1.0f,1.5f);
				}

				if(level >= 2){
					stem.GetComponent<stem>().nodeNumber = 1;transform.localScale = Vector3.one * Random.Range(1.5f,2.0f);
				}
			}

			//Flower two
			if(type == 1){
				
				//Level 1
				if(level == 0){
					stem.GetComponent<stem>().nodeNumber = 1;transform.localScale = Vector3.one * Random.Range(1.0f,1.5f);
				}
				
				if(level == 1){
					stem.GetComponent<stem>().nodeNumber = 2;transform.localScale = Vector3.one * Random.Range(1.5f,2.0f);
				}
				
				if(level == 2){
					stem.GetComponent<stem>().nodeNumber = 3;transform.localScale = Vector3.one * Random.Range(2.0f,2.5f);
				}
			}
			//Flower three
			if(type == 2){
				
				//Level 1
				if(level == 0){
					stem.GetComponent<stem>().nodeNumber = 1;transform.localScale = Vector3.one * Random.Range(2.0f,2.2f);
				}
				
				if(level == 1){
					stem.GetComponent<stem>().nodeNumber = 2;transform.localScale = Vector3.one * Random.Range(2.2f,2.5f);
				}
				
				if(level >= 2){
					stem.GetComponent<stem>().nodeNumber = 3;transform.localScale = Vector3.one * Random.Range(2.5f,2.7f);
				}
			}
			//Flower four
			if(type == 3){
				
				//Level 1
				if(level == 0){
					stem.GetComponent<stem>().nodeNumber = 2;transform.localScale = Vector3.one * Random.Range(2.0f,2.2f);
				}
				
				if(level == 1){
					stem.GetComponent<stem>().nodeNumber = 3;transform.localScale = Vector3.one * Random.Range(2.2f,2.5f);
				}
				
				if(level >= 2){
					stem.GetComponent<stem>().nodeNumber = 4;transform.localScale = Vector3.one * Random.Range(2.5f,2.7f);
				}
			}
		}
	}


	
	// Update is called once per frame
	void FixedUpdate () {

		if (plosion) {
			plosion = false;
		}

		//This is bit where I check to see if an explosion just happened!
		if (flowerAnimationManager != null && flowerAnimationManager.GetComponent<flowerAnimateManager> ().explodeNow) {
			plosion = true;
			explosionPosition = flowerAnimationManager.GetComponent<flowerAnimateManager> ().explosionPosition;

			//Calculate Angle for plant to lean away from explosion
			Vector3 targetvector = explosionPosition - transform.position;
			float Zangle = Mathf.Atan2(targetvector.x, targetvector.y) * Mathf.Rad2Deg;
			explosionRotation = Quaternion.Euler(new Vector3(0,0,Zangle)); 
		}
	}
}
