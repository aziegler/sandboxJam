using UnityEngine;
using System.Collections;

public class stem : MonoBehaviour
{

	public GameObject 	stemObject;
	public GameObject	leafObject;
	public GameObject 	stemEnd;
	public bool			finished;
	public int			nodeNumber;
	public bool			anotherNode;
	public float		angleVariation;
	public float 		angle;
	public float		animate;
	public float		endScale;

	// Use this for initialization
	void Start () 
	{

		//Make sure stem starts in ungrown state
		finished = false;
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		GameObject nextNode,secondNode,leaf,leaf2;

		//Grow Stem
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * endScale, 0.02f);

		//Check to see if we ready for another node
		if (finished == false) {
			if (transform.localScale.x > endScale/2) {
				finished = true;

				//Should I generate another node?
				if (nodeNumber > 0) {

					//Create 2 Leafs
					leaf = (GameObject)Instantiate (leafObject, stemEnd.transform.position, Quaternion.identity);
					//leaf2 = (GameObject)Instantiate (leafObject, transform.position, Quaternion.identity);

					//Set Up Leaf Variables
					leaf.GetComponent<leaf> ().angle = Random.Range (-9, 9)*10;
					leaf.GetComponent<leaf> ().animate = animate - 0.4f;
					//leaf2.GetComponent<leaf> ().angle = Random.Range (-9, 9)*10;
					//leaf2.GetComponent<leaf> ().animate = animate - 0.4f;

					int nextNodeNUmber = nodeNumber - 1;

					//Create Node
					nextNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						
					//Set up Node Variables
					nextNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
					nextNode.GetComponent<stem> ().angle = Random.Range (-angleVariation, angleVariation);
					nextNode.GetComponent<stem> ().animate = animate - 0.4f;
					nextNode.GetComponent<stem> ().finished = false;
					nextNode.GetComponent<stem> ().endScale = endScale - 0.15f;

					if (anotherNode == true) {
						//Create Another Node
						secondNode = (GameObject)Instantiate (stemObject, stemEnd.transform.position, Quaternion.identity);
						
						//Set up second node variables
						secondNode.GetComponent<stem> ().nodeNumber = nextNodeNUmber;
						secondNode.GetComponent<stem> ().angle = Random.Range (-angleVariation, angleVariation);
						secondNode.GetComponent<stem> ().animate = animate - 0.4f;
						secondNode.GetComponent<stem> ().finished = false;
						secondNode.transform.parent = transform;
						secondNode.GetComponent<stem> ().endScale = endScale - 0.15f;
					}

					//Parent new bits to me!
					nextNode.transform.parent = transform;
					leaf.transform.parent = transform;
					//leaf2.transform.parent = transform;

				}
			}
		}
		//Animate
		animate += 0.004f;
		if(animate >1)
		{
			animate -= 1;
		}
		
		transform.localEulerAngles = new Vector3(0,0,angle +( Mathf.Sin (animate * (Mathf.PI *2)) * 5));		
	}
}
