using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public static Planet Instance;
    CircleCollider2D circle;
    public GameObject PrefabFlower;
    public float FlowerSpawnAngle = 10f;

    void Awake ()
    {
        Instance = this;
        circle = GetComponent<CircleCollider2D>();
    }

    void Start ()
    {
        float total = 0f;

        /*do
        {
            GameObject go = GameObject.Instantiate(PrefabFlower);
            go.transform.position = Vector3.zero;
            go.transform.eulerAngles = new Vector3(0f, 0f, total);
            go.transform.Translate(Vector3.up * (circle.radius));
            go.transform.parent = transform;

            total += FlowerSpawnAngle;

        } while (total < 360f);*/
    }

    public float GetOrientedAngle (Transform t)
    {
        Vector3 dir = transform.position - t.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        return 180f - angle;
    }
}
