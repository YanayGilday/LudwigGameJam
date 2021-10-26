using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public GameObject player;
    public Transform doorsTransform;
    public Text enemyCountText;
    public Text worldCountText;

    private int[] xCords = {4, 19, 51, 70, 108, 136, 167, 199, 230, 266, 308};
    private int xCordIn = 0;
    public int[] zc = { 1, 2, 5, 0, 3, 10, 7, 10, 10, 15 };
    public int[] sc = { 0, 1, 3, 7, 3, 0, 7, 8, 10, 10 };
    public int[] gc = { 0, 0, 1, 3, 1, 0, 3, 4, 5, 8 };
    public int[] nc = { 0, 0, 0, 0, 1, 4, 1, 2, 4, 6};

    private bool isMidLevel = false;
    private bool talkedToPriest = false;
    private bool talkedToKnight = false;
    private int enemysInRoom;
    private int enemysAlive;

    public GameObject zp;
    public GameObject sp;
    public GameObject gp;
    public GameObject np;
    public GameObject heartp;

    public DialogTrigger priestDialog;
    public DialogTrigger knightDialog;

    public Transform endScreen;
    public Text creditText;
    public string[] endCredits;

    public void Update()
    {
        if (talkedToPriest == false && player.transform.position.x > -7)
        {
            priestDialog.TriggerDialog();
            talkedToPriest = true;
        }

        if (talkedToKnight == false && player.transform.position.x > 308)
        {
            talkedToKnight = true;
            knightDialog.TriggerDialog();
        }

        if (player.transform.position.x > xCords[xCordIn])
        {
            areDoorsOpen(false);
            if (xCordIn != 10) startLevel(xCords[xCordIn], xCords[xCordIn + 1] - 2, (xCordIn > 5), (xCordIn > 9), zc[xCordIn], sc[xCordIn], gc[xCordIn], nc[xCordIn]);
            xCordIn++;

        }
        
        if (isMidLevel && enemysAlive == 0)
        {
            isMidLevel = false;
            areDoorsOpen(true);
        }

        if (isMidLevel) enemyCountText.text = enemysAlive + " / " + enemysInRoom;
        else enemyCountText.text = "";

        FindClosestEnemy();

        worldCountText.text = ("Level " + xCordIn + " / 10");

    }

    void startLevel(int startpos, int endpos, bool doBats, bool doCrissCroos, int z, int s, int g, int n)
    {
        isMidLevel = true;


        List<GameObject> enemys = new List<GameObject>();
        enemysInRoom = z + s + g + n;
        enemysAlive = enemysInRoom;
        
        for (int i = 0; i < z; i++)
        {
            GameObject enemy = Instantiate(zp, this.transform.position, this.transform.rotation) as GameObject;
            enemys.Add(enemy);
        }
        for (int i = 0; i < s; i++)
        {
            GameObject enemy = Instantiate(sp, this.transform.position, this.transform.rotation) as GameObject;
            enemys.Add(enemy);
        }
        for (int i = 0; i < g; i++)
        {
            GameObject enemy = Instantiate(gp, this.transform.position, this.transform.rotation) as GameObject;
            enemys.Add(enemy);
        }
        for (int i = 0; i < n; i++)
        {
            GameObject enemy = Instantiate(np, this.transform.position, this.transform.rotation) as GameObject;
            enemys.Add(enemy);
        }

        foreach(GameObject spawnedEnemy in enemys)
        {
            int x = Random.Range(startpos + 1, endpos - 1);
            spawnedEnemy.transform.position = new Vector3(x, 5, 0);
        }
    }

    public void enemyKilled(int kill, GameObject thisEnemy)
    {
        if (isMidLevel)
        {
            enemysAlive = enemysAlive - kill;
            if (GameMode.CgameMode == 0)
            {
                int heart = Random.Range(1, 8);
                if (heart == 1 && kill > 0)
                {
                    GameObject food = Instantiate(heartp, thisEnemy.transform.position, thisEnemy.transform.rotation) as GameObject;
                }
            }
        }
    }

    void areDoorsOpen(bool isOpen)
    {
        if (isOpen == true) doorsTransform.position = new Vector3(0, 3, 0);
        else doorsTransform.position = new Vector3(0, 0, 0);
    }

    void FindClosestEnemy()
    {
        meEnemy[] allEnemys = GameObject.FindObjectsOfType<meEnemy>();
        foreach (meEnemy currentEnemy in allEnemys)
        {
            if (isMidLevel == false)
            {
                Destroy(currentEnemy.gameObject);
            }
        } 
    }

    public void endGame()
    {
        endScreen.position = new Vector3(1920/2, 1080/2, 0);
        StartCoroutine(CreditRoll());
    }

    IEnumerator CreditRoll()
    {
        int c = 0;
        for (int i = 0; i < endCredits.Length; i++)
        {
            creditText.text = endCredits[c];
            yield return new WaitForSeconds(5);
            c++;
        }
        this.gameObject.GetComponent<PlayerDeath>().loadTitleScene();
    }
}
