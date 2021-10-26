using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int dmg;
    public float speed =2f;
    private bool hitted = false;

    private Rigidbody2D rb;

    string tag;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rb.velocity = transform.right * speed;
    }

    public void BulletShot(int dmg1, float speed1, string direction1, string tag1)
    {
        dmg = dmg1;
        speed = speed1;
        tag = tag1;
    }

   void OnTriggerEnter2D (Collider2D collision)
    {
        if (hitted == false && collision.tag != tag)
        {
            rb.velocity = transform.right * 0;
            if (collision.TryGetComponent(out Life life))
            {
                life.Hurt(dmg);
            }
            Destroy(this.gameObject);
            hitted = true;
        }
    }
        

    private void hit()
    {
        Destroy(this.gameObject);
    }
}
