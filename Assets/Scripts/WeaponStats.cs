using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int dmg;
    public float speed;
    public float bulletPerSec;
    public int reloadTime;
    public int cartrageSize;
    public string Wname;
    public GameObject shootPoint;

    public Sprite avatar;
    public Sprite upSprite;
    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite downSprite;

    public void Pick()
    {
        this.GetComponent<Collider2D>().isTrigger = false;
        GameObject.Find("Gun").GetComponent<Attack>().PickWeapon(this.gameObject);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            this.GetComponent<Collider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    

}
