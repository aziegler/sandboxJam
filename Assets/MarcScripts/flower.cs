using UnityEngine;
using System.Collections;

public class flower : MonoBehaviour {

	public float endScale;
	float currentScale;
	float sway;
	float timeSinceBorn;

	// Use this for initialization
	void Start () {
		endScale = Random.Range (0.1f, 0.2f);
		sway = Random.Range (0, 1);
		timeSinceBorn = 0;

		transform.localScale = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Grow
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * endScale, 0.05f);

		//Animate
		timeSinceBorn ++;
		if (timeSinceBorn > 50) {
			sway += 0.01f;
			if (sway > 1) {
				sway = 0;
			}

			Vector3 flowerAngle = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Sin (sway * (Mathf.PI * 2)) * 4);

			transform.localEulerAngles = flowerAngle;
		}
			
	}
}
