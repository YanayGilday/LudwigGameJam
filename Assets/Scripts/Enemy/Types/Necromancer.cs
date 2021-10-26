using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Necromancer : MonoBehaviour
{
    private Transform playerTransform;
    private Transform enemyTransform;

    private float Rdirection;
    private string direction;

    public int dmg;
    public float bulletPerSec;
    public float bulletSpeed;
    private GameObject shootPoint;

    public bool ableToShoot = true;

    public GameObject prefabBullet;
    private GameObject bullet;

    private Sprite idle;
    private Sprite dead;
    public Collider2D deadCollider;
    private SpriteRenderer Body;
    private EnemyMovment enemyMovment;


    public void Start()
    {
        idle = Resources.Load<Sprite>("Sprites/Necromnacer/NecromancerSprite");
        dead = Resources.Load<Sprite>("Sprites/Necromnacer/NecromancerSpriteDead");
        Body = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyTransform = this.transform;
        shootPoint = this.transform.GetChild(1).gameObject;
        prefabBullet = Resources.Load<GameObject>("Bullets/NecroBullet");
        enemyMovment = GetComponent<EnemyMovment>();
    }

    private void FixedUpdate()
    {
        CheckIfDead();
        GunUpdate();
    }


    public void CheckIfDead()
    {
        if (this.GetComponent<Life>().dead == true)
        {
            enemyMovment.enabled = false;
            Body.GetComponent<SpriteRenderer>().sprite = dead;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            deadCollider.enabled = true;
            ableToShoot = false;
        }
    }
    void GunUpdate()
    {
        CheckDirection();
        shootPoint.transform.eulerAngles = new Vector3(0, 0, Rdirection);

        if (ableToShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    public void CheckDirection()
    {
        float px = playerTransform.position.x;
        float py = playerTransform.position.y;
        float ex = enemyTransform.position.x;
        float ey = enemyTransform.position.y;

        float m = (py - ey) / (px - ex);
        double dr = Mathf.Atan(m) * 180 / Mathf.PI;

        if (px < ex) Rdirection = (float)(dr + 180);
        else Rdirection = (float)dr;
    }

    IEnumerator Shoot()
    {
        bullet = Instantiate(prefabBullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
        bullet.transform.position = shootPoint.transform.position;
        bullet.GetComponent<Bullet>().BulletShot(dmg, bulletSpeed, direction, this.tag) ;
        ableToShoot = false;
        yield return new WaitForSeconds(1 / bulletPerSec);
        ableToShoot = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground") && this.GetComponent<Life>().dead == true)
        {
            deadCollider.isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
