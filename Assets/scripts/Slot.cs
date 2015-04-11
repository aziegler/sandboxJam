using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    public bool IsFertil;
    public Sprite FertilSprite;
    public Sprite DefaultSprite;
    SpriteRenderer sprite;

    void Start ()
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

            IsFertil = false;

            sprite.color = Color.black;
        }

        GameObject.Destroy(other.gameObject);
    }
}
