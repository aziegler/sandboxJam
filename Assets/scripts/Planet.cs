using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public static Planet Instance;
    CircleCollider2D circle;
    public GameObject PrefabFlower;
    public float FlowerSpawnAngle = 10f;

    public Sprite SlotFertilSprite;

    void Awake ()
    {
        Instance = this;
        circle = GetComponent<CircleCollider2D>();
    }

    void Start ()
    {
        float total = 0f;
        float numberOfSlot = 360f / FlowerSpawnAngle;
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

            GameObject go = new GameObject();
            go.name = "slot";
            go.layer = 14;

            BoxCollider2D box = go.AddComponent<BoxCollider2D>();            
            box.isTrigger = true;
            box.size = new Vector2(size / numberOfSlot, 1f);

            SpriteRenderer sprite = go.AddComponent<SpriteRenderer>();
            sprite.sortingOrder = 20;
            sprite.sortingLayerName = "Flowers";

            Slot slot = go.AddComponent<Slot>();           
            slot.FertilSprite = SlotFertilSprite;
            slot.IsFertil = false;


            go.transform.position = Vector3.zero;
            go.transform.eulerAngles = new Vector3(0f, 0f, total);
            go.transform.Translate(Vector3.up * (circle.radius));
            go.transform.parent = transform;


        } while (total < 360f);
    }

    public float GetOrientedAngle (Transform t)
    {
        Vector3 dir = transform.position - t.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        return 180f - angle;
    }
}
