using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public static Planet Instance;

    void Awake ()
    {
        Instance = this;
    }

    public float GetOrientedAngle (Transform t)
    {
        Vector3 dir = transform.position - t.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        return 180f - angle;
    }
}
