using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

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

    public Slot LeftNeighbor;
    public Slot RightNeighbor;

    void Start ()
    {
        int index = Random.Range(0, Sprites.Length);

        BackSprite.sprite = Sprites[index].BackSprite;
        FrontSprite.sprite = Sprites[index].FrontSprite;

        ShowSprites(false,true);
    }

    public Voices[] possibleVoices; 

    void OnTriggerEnter2D(Collider2D other)
    {
        Seed seed = other.GetComponent<Seed>();
        Spore spore = other.GetComponent<Spore>();

        if (null != seed && seed.IsCaptured)
            return;

        if ((IsFertil || GameManager.Instance.IsNewFlowerReplaceOldOne) && null != seed)
        {
            seed.IsCaptured = true;

            int previousLevel = 0;

            if(GameManager.Instance.IsNewFlowerReplaceOldOne)
            {
                if(null != PlantedFlower)
                {
                    FlowerRoot flower = PlantedFlower.GetComponent<FlowerRoot>();
                    FlowerRoot seedFlower = seed.Flower.GetComponent<FlowerRoot>();

                    if (flower.Level ==seedFlower.Level && flower.GrowthLevel<3)
                    {
                        previousLevel = flower.GrowthLevel;
                        PlantedFlower.GetComponent<FlowerRoot>().Kill();
                        CreateTheFlower(seed, previousLevel + 1,this.transform);
                    }
                }
                else
                {
                    CreateTheFlower(seed, 1,this.transform);
                }

				seed.StopFade();
                ScaleObject scale = seed.gameObject.AddComponent<ScaleObject>();
                scale.GetComponent<Rigidbody2D>().isKinematic = true;
                scale.Scale(0.2f, Vector3.zero, true, Center.position, AbsorbIsFinish); 
            }   
        }

        if(null != spore)
        {
			if(Time.time-spore.BirthTime > 0.1f)
			{
            	spore.Kill();
			}
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
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*0.1f);
			seed.Kill();
		}
        Spore spore = coll.gameObject.GetComponent<Spore>();
        if(null != spore && spore.IsFlying)
        {
			if(Time.time - spore.BirthTime>0.1f)
			{
				spore.Kill();
			}
        }
    }

    public void HitByLaser(Vector3 hitVector)
    {
        if(!IsFertil)
        {
            IsFertil = true;
            ShowSprites(true);
        }
        else if(IsFertil)
        {
            //
        }
    }

    public void ShowSprites(bool show,bool force=false)
    {
		if (force) 
		{
			ApplyScaleOnSprite(BackSprite,Vector3.one,Vector3.zero,1f);
			ApplyScaleOnSprite(FrontSprite,Vector3.one,Vector3.zero,1f);
		}
		else
		{
			Vector3 targetScale= show ? Vector3.one: Vector3.zero;
			StopCoroutine ("Scale");
			StartCoroutine ("Scale", targetScale);
		}

        GetComponent<BoxCollider2D>().isTrigger = show;
    }

	void ApplyAlphaOnSprite(SpriteRenderer sprite, float startAlpha, float targetAlpha, float  t)
	{
		Color c = sprite.color;
		c.a = Mathf.Lerp (startAlpha, targetAlpha, t);
		sprite.color = c;
	}

	void ApplyScaleOnSprite(SpriteRenderer sprite, Vector3 startScale, Vector3 targetScale, float t)
	{
		Vector3 scale = sprite.transform.localScale;
		scale = Vector3.Lerp (startScale, targetScale, t);
		sprite.transform.localScale = scale;
	}

	IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = BackSprite.color.a;
		float t = 0f;
		float startTime = Time.time;
		float delay = Planet.Instance.SlotScaleDuration;

		do {
			t = Mathf.Clamp ((Time.time - startTime) / delay, 0f, 1f);

			ApplyAlphaOnSprite(BackSprite,startAlpha,targetAlpha,t);
			ApplyAlphaOnSprite(FrontSprite,startAlpha,targetAlpha,t);
			yield return null;
		} while(t<1f);
	}

	IEnumerator Scale(Vector3 targetScale)
	{
		yield return new WaitForSeconds (Planet.Instance.SlotScaleDelay);
		Vector3 startScale = BackSprite.transform.localScale;
		float t = 0f;
		float startTime = Time.time;
		float delay = Planet.Instance.SlotScaleDuration;
		
		do {
			t = Mathf.Clamp ((Time.time - startTime) / delay, 0f, 1f);
			
			ApplyScaleOnSprite(BackSprite,startScale,targetScale,t);
			ApplyScaleOnSprite(FrontSprite,startScale,targetScale,t);
			yield return null;
		} while(t<1f);
	}

    void CreateTheFlower (Seed seed , int growthlevel, Transform slot)
    {
        GameObject flower = GameObject.Instantiate(seed.Flower.gameObject);
        flower.transform.position = transform.position;
        flower.transform.rotation = transform.rotation;
        flower.transform.parent = Planet.Instance.transform;
        flower.GetComponent<FlowerRoot>().GrowthLevel = growthlevel;
        flower.GetComponent<FlowerRoot>().Slot = slot;
        flower.GetComponent<flowerLevel>().level = (growthlevel - 1);
        PlantedFlower = flower;

        var flowerObject = PlantedFlower.GetComponent<FlowerRoot>();
        GameManager.Instance.FlowerCreated(flowerObject.Level);
       
        flowerObject.Voice = Instantiate(possibleVoices[Random.Range(0, possibleVoices.Length)]);
        flowerObject.Voice.transform.SetParent(flowerObject.transform);


		int point = flower.GetComponent<FlowerRoot> ().Level + 1;
		if (flower.GetComponent<FlowerRoot> ().Level == 3) 
		{
			point*=2;
		}

		GameManager.Instance.Score += point;
        if(seed.firstParent != null)
            seed.firstParent.Sterile = true;

        if(seed.secondParent != null)
            seed.secondParent.Sterile = true;
        /*if (flowerObject.Level > GameManager.Instance.currentMaxLevel)
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
        }*/

        IsFertil = false;
    }
}
