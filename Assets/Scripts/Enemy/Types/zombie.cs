using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class zombie : MonoBehaviour
{
    public int timeToRevive = 10;
    public int dmg;
    public int recoilTime;

    public Sprite idle;
    public Sprite dead;
    public SpriteRenderer zombieBody;
    public EnemyMovment enemyMovment;

    private levelManager levelManager;

    public Collider2D collider;
    public Collider2D deadCollider;

    public void Start()
    {
        idle = Resources.Load<Sprite>("Sprites/Zombie/ZombieSpriteIdle");
        dead = Resources.Load<Sprite>("Sprites/Zombie/ZombieSpriteDead");
        zombieBody = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        enemyMovment = GetComponent<EnemyMovment>();
        levelManager = GameObject.Find("GameManager").GetComponent<levelManager>();
    }

    private void FixedUpdate()
    {
        CheckIfDead();
    }

    public void CheckIfDead()
    {
        if (this.GetComponent<Life>().dead == true)
        {
            StartCoroutine("StartRevive");
            enemyMovment.enabled = false;
            zombieBody.GetComponent<SpriteRenderer>().sprite = dead;
            collider.enabled = false;
            deadCollider.enabled = true;
        }
    }

    IEnumerator StartRevive()
    {
        yield return new WaitForSeconds(timeToRevive);
        Revive();
    }

    public void Revive()
    {
        StopCoroutine("StartRevive");
        if (this.GetComponent<Life>().life <= 0)
        {
            this.GetComponent<Life>().life = 25;
            this.GetComponent<Life>().dead = false;
            this.GetComponent<Life>().died = false;
        }
        enemyMovment.enabled = true;
        zombieBody.GetComponent<SpriteRenderer>().sprite = idle;
        collider.enabled = true;
        deadCollider.enabled = false;
        deadCollider.isTrigger = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y + 2, 0);


        levelManager.enemyKilled(-1, this.gameObject);
        StopCoroutine("StartRevive");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.name == ("Player") && this.GetComponent<Life>().dead == false)
        {
            collision.collider.GetComponent<Life>().Hurt(dmg);
        }

        if (collision.collider.CompareTag("ground") && this.GetComponent<Life>().dead == true)
        {
            deadCollider.isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
