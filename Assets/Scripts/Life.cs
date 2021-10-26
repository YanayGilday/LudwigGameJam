using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float lifeMax;
    public float life;
    public bool dead = false;
    public bool died = false;
    
    public string dieInCodeName;

    private levelManager levelManager;

    public void Start()
    {
        life = lifeMax;
        levelManager = GameObject.Find("GameManager").GetComponent<levelManager>();
    }

    public void Update()
    {
        if (life <= 0)
        {
            enemyDied();
            dead = true;
        }
        else
        {
            dead = false;
        }

        if (this.gameObject.GetComponent<Transform>().position.y < -30)
        {
            life = -1000f;
        }

    }
    public void Hurt(int damageTaken)
    {
        life = life -damageTaken;
    }

    void enemyDied()
    {
        if (died == false && this.tag == "enemy")
        {
            levelManager.enemyKilled(1, this.gameObject);
            died = true;
        }
    }
    
}

