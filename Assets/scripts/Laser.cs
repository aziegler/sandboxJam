using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public Sprite _wideLaser;
    public Sprite _aim;
    public Transform Bomb;
    public Transform Planet;
    public Transform Explosion;
    public LayerMask Mask;
    private Transform _explosion;

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
        GameManager.Instance.ShakeCam();
         GetComponent<BoxCollider2D>().enabled = true;

        Planet = GameObject.FindGameObjectWithTag("Planet").transform;
   /*     var spriteRenderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _wideLaser;*/

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
                if (GameManager.Instance.CurrentZoom > 1f && GameManager.Instance.CurrentZoom < 2f)
                {
                    if(hitVector.x >0)
                        slot.LeftNeighbor.HitByLaser(hitVector);
                    else
                        slot.RightNeighbor.HitByLaser(hitVector);

                }
                else if (GameManager.Instance.CurrentZoom > 2f)
                {
                    slot.LeftNeighbor.HitByLaser(hitVector);
                    slot.RightNeighbor.HitByLaser(hitVector);
                    if (hitVector.x > 0)
                        slot.LeftNeighbor.LeftNeighbor.HitByLaser(hitVector);
                    else
                        slot.RightNeighbor.RightNeighbor.HitByLaser(hitVector);
                }
                if (_explosion != null)
                {
                    DestroyMud();
                    CancelInvoke("DestroyMud");
                }
                _explosion = (Transform)
                    Instantiate(Explosion, new Vector3(raycastHit2D.point.x, raycastHit2D.point.y, -6),
                        Quaternion.identity);
                _explosion.SetParent(slot.transform);
            }

            //Bomb
            var bomb = (Transform)Instantiate(Bomb, new Vector3(raycastHit2D.point.x, raycastHit2D.point.y), Quaternion.identity);
            
            GameObject flowerAnimationManager = GameObject.FindGameObjectWithTag("AnimManager");
            if(flowerAnimationManager != null){
                flowerAnimationManager.GetComponent<flowerAnimateManager>().InitExplosion(bomb.transform.position);
            }

            
            

            Invoke("DestroyMud",1f);
            bomb.GetComponent<Bomb>().Init();
            bomb.transform.parent = Planet;

            var circleCollider2D = bomb.GetComponent<CircleCollider2D>();
            circleCollider2D.radius = circleCollider2D.radius + GameManager.Instance.CurrentZoom/3f;
        }

        Invoke("Back", 0.1f);
    }

    private void DestroyMud()
    {
        Destroy(_explosion.gameObject);
        _explosion = null;
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

            if (GameManager.Instance.IsLaserKillFlower)
            {
                flower.Kill();
            }
        }
        else
        {
            Debug.LogWarning("Laser hit a no flower root stuff " + other.name);
        }
    }    
}