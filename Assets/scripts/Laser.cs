using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public Sprite _wideLaser;
    public Sprite _aim;
    public Transform Bomb;
    public Transform Planet;
    public LayerMask Mask;
   

    public void Shoot()
    {
        Planet = GameObject.FindGameObjectWithTag("Planet").transform;
        var spriteRenderer =(SpriteRenderer) gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _wideLaser;

        var bomb =(Transform) Instantiate(Bomb, new Vector3(gameObject.GetComponent<Transform>().localPosition.x,0.5f,0f), Quaternion.identity);
        bomb.GetComponent<Bomb>().Init();
        bomb.transform.parent = Planet;
        Invoke("Back",0.1f);
    }

    public void ShootRound()
    {
        GetComponent<BoxCollider2D>().enabled = true;

        Planet = GameObject.FindGameObjectWithTag("Planet").transform;
        var spriteRenderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _wideLaser;

        this.GetComponent<BoxCollider2D>().enabled = true;

        Vector2 origin = new Vector2(Planet.transform.position.x, 20f);
        Vector2 direction = new Vector2(0f, -1f);

        //var raycastHit2D = Physics2D.Raycast(transform.position, Planet.transform.position - transform.position, Mask);
        var raycastHit2D = Physics2D.Raycast(origin, direction, 10f, Mask);
        if (null != raycastHit2D.collider)
        {
            Slot slot = raycastHit2D.collider.gameObject.GetComponent<Slot>();
            if(null != slot)
            {
                Vector3 hitVector = slot.transform.InverseTransformPoint(raycastHit2D.point);
                slot.HitByLaser(hitVector);
            }

            //Bomb
            var bomb = (Transform)Instantiate(Bomb, new Vector3(raycastHit2D.point.x, raycastHit2D.point.y), Quaternion.identity);
            bomb.GetComponent<Bomb>().Init();
            bomb.transform.parent = Planet;                       
        }

        Invoke("Back", 0.1f);
    }

    public void Update()
    {
       
    }

    public void Back()
    {
        var spriteRenderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
       spriteRenderer.sprite = _aim;
       GetComponent<BoxCollider2D>().enabled = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        FlowerFinder ff = other.GetComponent<FlowerFinder>();
        if (null != ff)
        {
            FlowerRoot flower = ff.Flower;
            flower.Kill();
        }
        else
        {
            Debug.LogWarning("Laser hit a no flower root stuff " + other.name);
        }
    }    
}