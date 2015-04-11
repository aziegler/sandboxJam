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
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.enabled = true;
                }
            }
            else
            {
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.enabled = false;
                }
            }
        }
    }
    public GameObject PlantedFlower;

    public SpriteRenderer[] sprites;

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

            if (PlantedFlower.GetComponent<Flower>().Level > GameManager.Instance.currentMaxLevel)
            {
                GameManager.Instance.currentMaxLevel = PlantedFlower.GetComponent<Flower>().Level;
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
        }

        GameObject.Destroy(other.gameObject);
    }

    public void HitByLaser(Vector3 hitVector)
    {
        if (null != PlantedFlower)
        {
            IsFertil = false;

            if (Mathf.Abs(hitVector.x) <= 0.05)
            {
                GameObject.Destroy(PlantedFlower);
                PlantedFlower = null;
            }
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
