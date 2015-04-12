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
        }
    }
    public GameObject PlantedFlower;

    public SpriteRenderer BackSprite;
    public SpriteRenderer FrontSprite;

    public SpriteBackFront[] Sprites;

    public Transform Center;

    void Start ()
    {
        int index = Random.Range(0, Sprites.Length);

        BackSprite.sprite = Sprites[index].BackSprite;
        FrontSprite.sprite = Sprites[index].FrontSprite;

        ShowSprites(false);
    }

    public Voices[] possibleVoices; 

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

            var flowerObject = PlantedFlower.GetComponent<Flower>();

            flowerObject.Voice = Instantiate(possibleVoices[Random.Range(0, possibleVoices.Length)]);
            flowerObject.Voice.transform.SetParent(flowerObject.transform);
            if (flowerObject.Level > GameManager.Instance.currentMaxLevel)
            {
                GameManager.Instance.currentMaxLevel = flowerObject.Level;
                if (GameManager.Instance.currentMaxLevel == 1)
                {
                    GameManager.Instance.SwitchSoundSource(2);
                }

                if (GameManager.Instance.currentMaxLevel == 2)
                {
                    GameManager.Instance.SwitchSoundSource(3);
                }
            }

            IsFertil = false;

            ScaleObject scale = seed.gameObject.AddComponent<ScaleObject>();
            scale.GetComponent<Rigidbody2D>().isKinematic = true;
            
            scale.Scale(0.2f, Vector3.zero, true, Center.position, AbsorbIsFinish);     
        }
    }

    void AbsorbIsFinish()
    {
        IsFertil = false;

        ShowSprites(false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Seed seed = coll.gameObject.GetComponent<Seed>();
        if (null != seed)
        {
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*2f);
            GameObject.Destroy(coll.gameObject, 0.4f);
        }
    }

    public void HitByLaser(Vector3 hitVector)
    {
        if (null != PlantedFlower)
        {
            IsFertil = false;
            ShowSprites(false);
            if (Mathf.Abs(hitVector.x) <= 0.5)
            {
                var component = PlantedFlower.GetComponent<Flower>();
                component.Kill();  
                Destroy(PlantedFlower);
                PlantedFlower = null;
            }
        }
        else if(!IsFertil)
        {
            IsFertil = true;

            ShowSprites(true);
        }
        else if(IsFertil)
        {
            //
        }
    }

    void ShowSprites(bool show)
    {
        BackSprite.enabled = show;
        FrontSprite.enabled = show;
        GetComponent<BoxCollider2D>().isTrigger = show;
    }
}
