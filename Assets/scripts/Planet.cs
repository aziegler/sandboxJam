using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public static Planet Instance;
    CircleCollider2D circle;
    public GameObject PrefabFlower;
    public float FlowerSpawnAngle = 10f;

    public GameObject SlotPrefab;

    void Awake ()
    {
        Instance = this;
        circle = GetComponent<CircleCollider2D>();
    }

    void Start ()
    {
        float total = 0f;
        float numberOfSlot = 360f / FlowerSpawnAngle;
        Slot firstSlot = null;
        Slot prevSlot = null;
        do
        {
            /*
            GameObject go = GameObject.Instantiate(PrefabFlower);
            go.transform.position = Vector3.zero;
            go.transform.eulerAngles = new Vector3(0f, 0f, total);
            go.transform.Translate(Vector3.up * (circle.radius));
            go.transform.parent = transform;*/

            total += FlowerSpawnAngle;

            float size = 2 * circle.radius * Mathf.PI;

            GameObject go = GameObject.Instantiate(SlotPrefab);
            go.name = "slot";
            go.layer = 14;

            BoxCollider2D box = go.GetComponent<BoxCollider2D>();            
            box.isTrigger = true;
            box.size = new Vector2(size / numberOfSlot, 1f);

            Slot slot = go.GetComponent<Slot>();
            slot.IsFertil = false;
            if (firstSlot == null)
                firstSlot = slot;
            if (prevSlot != null)
            {
                prevSlot.RightNeighbor = slot;
                slot.LeftNeighbor = prevSlot;             
            }
            prevSlot = slot;

            go.transform.position = Vector3.zero;
            go.transform.eulerAngles = new Vector3(0f, 0f, total);
            go.transform.Translate(Vector3.up * (circle.radius));
            go.transform.parent = transform;


        } while (total < 360f);
        prevSlot.RightNeighbor = firstSlot;
        firstSlot.LeftNeighbor = prevSlot;
    }

    public float GetOrientedAngle (Transform t)
    {
        Vector3 dir = transform.position - t.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        return 180f - angle;
    }
}
