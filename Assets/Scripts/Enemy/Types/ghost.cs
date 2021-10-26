using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;

public class ghost : MonoBehaviour
{
    private Transform playerTransform;
    private Transform enemyTransform;

    private float Rdirection;
    private string direction;

    public int dmg;
    public float bulletPerSec;
    public float bulletSpeed;
    private GameObject shootPoint;

    private bool ableToShoot = true;

    private GameObject prefabBullet;
    private GameObject bullet;

    private SpriteRenderer Body;
    public AIPath enemyMovment;

    public SpriteRenderer thx;
    public SpriteRenderer body;

    public void Start()
    {
        Body = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyTransform = this.transform;
        shootPoint = this.transform.GetChild(1).gameObject;
        prefabBullet = Resources.Load<GameObject>("Bullets/GhostBullet");
        enemyMovment = GetComponent<AIPath>();
        this.GetComponent<AIDestinationSetter>().target = playerTransform;
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
            this.GetComponent<AIDestinationSetter>().target = null;
            ableToShoot = false;
            StartCoroutine(Die());
        }
    }
    void GunUpdate()
    {
        CheckDirection();
        shootPoint.transform.eulerAngles = new Vector3(0, 0, Rdirection);

        if (ableToShoot == true)
        {
            ableToShoot = false;
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
        bullet.GetComponent<Bullet>().BulletShot(dmg, bulletSpeed, direction, this.tag);

        yield return new WaitForSeconds(1 / bulletPerSec);

        ableToShoot = true;
    }

    IEnumerator Die()
    {
        thx.enabled = true;
        body.enabled = false;
        yield return new WaitForSeconds(2);
        /*for (int i = 255; i > 0; i--)
        { 
            Debug.Log("1");
            body.color = new Color(255, 255, 255, i);
            yield return new WaitForSeconds(3/255);
        }
        yield return new WaitForSeconds(2);
        for (int i = 255; i > 0; i--)
        {
            Debug.Log("2");
            thx.color = new Color(255, 255, 255, i);
            yield return new WaitForSeconds(2 / 255);
        }*/

        Destroy(this.gameObject);
    }
}
