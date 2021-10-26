using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    public GameObject deathScreen;
    public Slider health;

    public GameObject lifeBar;
    public GameObject FlifeBar;

    public Sprite FdeathSprite;

    public void Update()
    {
        setHealthBar();
        checkIfDead();
        float lifePrecentage = player.GetComponent<Life>().life / player.GetComponent<Life>().lifeMax;
        health.value = ((lifePrecentage * 0.846f) + 0.154f);

    }

    public void setHealthBar ()
    {
        if (GameMode.CgameMode == 2) lifeBar.transform.position = new Vector3(-5000, -5000, 0);
        else FlifeBar.transform.position = new Vector3(-5000, -5000, 0);
    }

    public void checkIfDead()
    {
        if (player.GetComponent<Life>().dead == true)
        {
            player.GetComponent<Movment>().enabled = false;
            if (GameMode.CgameMode == 2) StartCoroutine(FoddianDeath());
            else deathScreen.transform.position = new Vector3(1920 / 2, 1080 / 2, 0);
        }
        else
        {
            deathScreen.transform.position = new Vector3(-5000, -5000, 0);
            player.GetComponent<Movment>().enabled = true;
        }

        if (GameMode.CgameMode == 2 && player.GetComponent<Life>().life < player.GetComponent<Life>().lifeMax)
        {
            player.GetComponent<Life>().life = -1000;
        }

        if (GameMode.CgameMode == 3)
        {
            if (player.GetComponent<Life>().life < 100000)
            {
                player.GetComponent<Life>().life = 100000;

            }

            gun.GetComponent<Attack>().dmg = 500000;
            gun.GetComponent<Attack>().bulletPerSec = 60;

        }
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Debug.Log("reloaded");
    }

    public void loadTitleScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("TitleScreen");
    }

    IEnumerator FoddianDeath()
    {
        FlifeBar.GetComponent<Image>().sprite = FdeathSprite;
        yield return new WaitForSeconds(3);
        deathScreen.transform.position = new Vector3(1920 / 2, 1080 / 2, 0);
    }
}
