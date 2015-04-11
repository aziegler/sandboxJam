using UnityEngine;

public class Laser : MonoBehaviour
{
    public Sprite _wideLaser;
    public Sprite _aim;
    public Transform Bomb;

    public void Shoot()
    {
        var spriteRenderer =(SpriteRenderer) gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _wideLaser;

        var bomb =(Transform) Instantiate(Bomb, new Vector3(gameObject.GetComponent<Transform>().localPosition.x,0.5f,0f), Quaternion.identity);
        bomb.GetComponent<Bomb>().Init();
        Invoke("Back",1f); 
        
    }

    public void Back()
    {
        var spriteRenderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
       spriteRenderer.sprite = _aim;

    }

    
}