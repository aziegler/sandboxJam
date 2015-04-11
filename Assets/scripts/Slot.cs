using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    bool isFertil;
    public bool IsFertil
    {
        get { return isFertil; }

        set
        {
            isFertil = value;
            if(IsFertil)
            {
                sprite.enabled = true;
                sprite.sprite = FertilSprite;
            }
            else
            {
                sprite.enabled = false;
            }
        }
    }
    public GameObject PlantedFlower;

    public Sprite FertilSprite;
    SpriteRenderer sprite;

    void Awake ()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(IsFertil)
        {
            Seed seed = other.GetComponent<Seed>();
            GameObject flower = GameObject.Instantiate(seed.Flower.gameObject);
            flower.transform.position = transform.position;
            flower.transform.rotation = transform.rotation;
            flower.transform.parent = Planet.Instance.transform;

            PlantedFlower = flower;

            IsFertil = false;
        }

        GameObject.Destroy(other.gameObject);
    }

    public void HitByLaser()
    {
        if (null != PlantedFlower)
        {
            IsFertil = false;
            GameObject.Destroy(PlantedFlower);
            PlantedFlower = null;
        }
        else if(!IsFertil)
        {
            IsFertil = true;
        }
        else if(IsFertil)
        {
            //
        }
    }
}
