using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Transform TitleAnchor;
    public Transform HelpAnchor;

    public bool isOnHelp;
    public bool readyToPlay;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	    if(Input.anyKeyDown || Input.GetButtonDown("Fire1"))
        {
            if(!isOnHelp)
            {
                isOnHelp = true;
                StartCoroutine("MoveTo", this.HelpAnchor.position);
            }
            else if(readyToPlay)
            {
                Application.LoadLevel(1);
            }
        }
	}

    IEnumerator MoveTo(Vector3 target)
    {
        Vector3 startPosition = Camera.main.transform.position;
        float startTime = Time.time;
        float delay = 0.5f;
        float t = 0f;

        do
        {
            t = (Time.time - startTime) / delay;
            Vector3 newPosition = Vector3.Lerp(startPosition, target, t);
            Camera.main.transform.position = newPosition;
            yield return new WaitForEndOfFrame();
        } while (t < 1f);

        readyToPlay = true;
    }
}
