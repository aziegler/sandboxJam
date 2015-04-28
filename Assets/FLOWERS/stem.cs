using UnityEngine;
using System.Collections;

public class stem : MonoBehaviour
{
	public GameObject	root;
	public GameObject 	stemObject;
	public GameObject	leafObject;
	public GameObject 	stemEnd;
	public GameObject	flowerHead;
	public bool			finished;
	public int			nodeNumber;
	public int			splitNode;
	public float		angleVariation;
	public float 		angle;
	public float		animate;
	public float		endScale;
	public float		plosion;
	public bool			amIBaseOfPlant;

	// Use this for initialization
	void Start () 
	{
		//Make sure stem starts in ungrown state
		finished = false;
		transform.localScale = Vector3.zero;

		//Find Root Object
		if (transform.parent.name == "FlowerOne(Clone)" || transform.parent.name == "FlowerOne" ||
			transform.parent.name == "FlowerTwo(Clone)" || transform.parent.name == "FlowerTwo" ||
			transform.parent.name == "FlowerThree(Clone)" || transform.parent.name == "FlowerThree" ||
			transform.parent.name == "FlowerFour(Clone)" || transform.parent.name == "FlowerFour")
		{
			root = transform.parent.gameObject;
		}

		plosion = 0;

		//Set up stuff based on which level is defined in 'root'
		if (root != null) {

			//Flower One
			if(root.GetComponent<flowerLevel>().type == 0)
			{
				//Flower One Level 0
				if(root.GetComponent<flowerLevel>().level == 0){
					splitNode = 0; angleVariation = 50;
				}

				//Flower One Level 1
				if(root.GetComponent<flowerLevel>().level == 1){
					splitNode = 1; angleVariation = 50;
				}

				//Flower One Level 2
				if(root.GetComponent<flowerLevel>().level == 2){
					splitNode = 2; angleVariation = 50;
				}

				//Flower Two
				if(root.GetComponent<flowerLevel>().type == 1)
				{
					//Flower One Level 0
					if(root.GetComponent<flowerLevel>().level == 0){
						splitNode = 1; angleVariation = 50;
					}
					
					//Flower One Level 1
					if(root.GetComponent<flowerLevel>().level == 1){
						splitNode = 0; angleVariation = 50;
					}
					
					//Flower One Level 2
					if(root.GetComponent<flowerLevel>().level >= 2){
						splitNode = 1; angleVariation = 50;
					}
				}

				//Flower Three
				if(root.GetComponent<flowerLevel>().type == 2)
				{
					//Flower One Level 0
					if(root.GetComponent<flowerLevel>().level == 0){
						splitNode = 1; angleVariation = 50;
					}
					
					//Flower One Level 1
					if(root.GetComponent<flowerLevel>().level == 1){
						splitNode = 0; angleVariation = 50;
					}
					
					//Flower One Level 2
					if(root.GetComponent<flowerLevel>().level >= 2){
						splitNode = 1; angleVariation = 50;
					}
				}
				//Flower Four
				if(root.GetComponent<flowerLevel>().type == 3)
				{
					//Flower One Level 0
					if(root.GetComponent<flowerLevel>().level == 0){
						splitNode = 1; angleVariation = 50;
					}
					
					//Flower One Level 1
					if(root.GetComponent<flowerLevel>().level == 1){
						splitNode = 1; angleVariation = 50;
					}
					
					//Flower One Level 2
					if(root.GetComponent<flowerLevel>().level >= 2){
						splitNode = 1; angleVariation = 70;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		GameObject nextNode,secondNode,thirdNode,leaf,Head;

		//Grow Stem
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * endScale, 0.04f);

		//Check to see if we ready for another node
		if (finished == false) {
			if (transform.localScale.x > endScale/2) {
				finished = true;

				//Should I generate another node?
				if (nodeNumber > 0) {

					//Create Leaf
					leaf = (GameObject)Instantiate (leafObject, stemEnd.transform.position, Quaternion.identity);
					leaf.GetComponent<leaf> ().angle = Random.Range (-9, 9)*10;
					leaf.GetComponent<leaf> ().animate = animate - 0.4f;

					int nextNodeNUmber = nodeNumber - 1;

					//Create Node
					if(splitNode == 0){

						float randomAngle = Random.Range (-angleVariation, angleVariation);
						nextNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						nextNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						nextNode.GetComponent<stem> ().angle = randomAngle;
						nextNode.GetComponent<stem> ().animate = animate - 0.4f;
						nextNode.GetComponent<stem> ().finished = false;
						nextNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						nextNode.GetComponent<stem> ().amIBaseOfPlant = false;

						nextNode.transform.parent = transform;
					}

					if (splitNode == 1) {
						float randomAngle = Random.Range (-angleVariation, angleVariation);

						//Create Node
						nextNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						nextNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						nextNode.GetComponent<stem> ().angle = randomAngle - Random.Range (35, 45);
						nextNode.GetComponent<stem> ().animate = animate - 0.4f;
						nextNode.GetComponent<stem> ().finished = false;
						nextNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						nextNode.GetComponent<stem> ().amIBaseOfPlant = false;

						//Create Another Node
						secondNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						secondNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						secondNode.GetComponent<stem> ().angle = randomAngle + Random.Range (35, 45);
						secondNode.GetComponent<stem> ().animate = animate - 0.4f;
						secondNode.GetComponent<stem> ().finished = false;
						secondNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						secondNode.GetComponent<stem> ().amIBaseOfPlant = false;

						nextNode.transform.parent = transform;
						secondNode.transform.parent = transform;
					}

					if (splitNode == 2) {
						float randomAngle = Random.Range (-angleVariation, angleVariation);

						//Create Node
						nextNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						nextNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						nextNode.GetComponent<stem> ().angle = randomAngle - Random.Range (55, 65);
						nextNode.GetComponent<stem> ().animate = animate - 0.4f;
						nextNode.GetComponent<stem> ().finished = false;
						nextNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						nextNode.GetComponent<stem> ().amIBaseOfPlant = false;

						//Create Another Node
						secondNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						secondNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						secondNode.GetComponent<stem> ().angle = randomAngle;
						secondNode.GetComponent<stem> ().animate = animate - 0.4f;
						secondNode.GetComponent<stem> ().finished = false;
						secondNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						secondNode.GetComponent<stem> ().amIBaseOfPlant = false;

						//Create Third Node
						thirdNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						thirdNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						thirdNode.GetComponent<stem> ().angle = randomAngle + Random.Range (55, 65);
						thirdNode.GetComponent<stem> ().animate = animate - 0.4f;
						thirdNode.GetComponent<stem> ().finished = false;
						thirdNode.GetComponent<stem> ().endScale = endScale - 0.15f;
						thirdNode.GetComponent<stem> ().amIBaseOfPlant = false;

						nextNode.transform.parent = transform;
						secondNode.transform.parent = transform;
						thirdNode.transform.parent = transform;
					}
				
					//Parent leaf to me!
					leaf.transform.parent = transform;
				}

				if(nodeNumber == 0)
				{
					//FlowerHaad
					Head = (GameObject)Instantiate (flowerHead, stemEnd.transform.position, Quaternion.identity);
					Head.transform.localEulerAngles = new Vector3(0,0,Random.Range(-15,15));

					//Parent Flower Head
					Head.transform.parent = transform;
				}
			}
		}
		//Animate
	   
	    if(root != null){
            var flowerLevel = root.GetComponent<flowerLevel>();
			if (flowerLevel != null && flowerLevel.plosion != null){

				//Make it ripple across planet based on distance from explosion
				plosion = 2 + (int)Vector3.Distance(transform.position,flowerLevel.explosionPosition)/2;
			}
		}

		if (plosion > 2) {
			plosion -= 1;
		}
		//Explosion Animation
		if(plosion >0 && plosion <= 2){

			if(plosion >=1 ){
				//In Explosion animation
				float plosionSmoothed = Mathf.SmoothStep(0,1,2-plosion);
				animate += 0.004f + (plosionSmoothed/10);
				Quaternion targetRotation = Quaternion.Euler(new Vector3(0,0,(Mathf.Sin (animate * (Mathf.PI *2)) * (5 +(plosionSmoothed*5)))));
				if(amIBaseOfPlant){
                    var flowerLevel = root.GetComponent<flowerLevel>();
					//Point at bomb position				
					targetRotation = flowerLevel.explosionRotation;
				}

				transform.localRotation = Quaternion.Lerp (transform.localRotation ,targetRotation,plosionSmoothed);
				plosion -= 0.3f;
			}
			if(plosion <1){
				//Out Explosion Animation
				float plosionSmoothed = Mathf.SmoothStep(0,1,1-plosion);
				animate += 0.004f + ((1-plosionSmoothed)/10);
				Quaternion targetAngle = Quaternion.Euler(new Vector3(0,0,Mathf.Sin (animate * (Mathf.PI *2)) * (5 + ((1-plosionSmoothed)*5))));
				if(amIBaseOfPlant){

					//Point at bomb position				
                    targetAngle = root.GetComponent<flowerLevel>().explosionRotation;
				}

				transform.localRotation = Quaternion.Lerp (targetAngle,Quaternion.Euler(new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5))),plosionSmoothed);
				plosion -= 0.01f;if(plosion <0){plosion = 0;}
			}

		} else {

			//Swaying normally
			animate += 0.004f;if(animate >1){animate -= 1;}
			transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));
		}

				
	}
}
