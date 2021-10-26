using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public GameObject GMWindow;
    public GameObject TWindow;
    public Animator animator;
    public Animator animator3;
    public Animator tAnimator;

    public void loadGameMods()
    {
        GMWindow.transform.position = new Vector3(1920/2, 1080/2, 0);
        animator.SetBool("open", true);
    }

    public void unloadGameMods()
    {
        GMWindow.transform.position = new Vector3(10000, 10000, 0);
        animator.SetBool("open", false);
        unloadGameMod3();
    }

    public void loadGameMod3()
    {
        animator3.SetBool("3isOpen", true);
    }

    public void unloadGameMod3()
    {
        animator3.SetBool("3isOpen", false);
    }

    public void loadTutorial()
    {
        TWindow.transform.position = new Vector3(1920 / 2, 1080 / 2, 0);
        tAnimator.SetBool("isOpen", true);
    }

    public void unloadTutorial()
    {
        TWindow.transform.position = new Vector3(10000, 10000, 0);
        tAnimator.SetBool("isOpen", false);
        unloadGameMod3();
    }

    public void exit()
    {
        Application.Quit();
    }

    public void loadWorld()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("World");
    }
}
