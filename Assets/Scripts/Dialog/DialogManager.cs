using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Dialogue

public class DialogManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;
    public Image avatarImage;
    public Rigidbody2D playerRB;

    public Animator animator;

    private Queue<string> sentences;

    public bool isInDialog = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void Update()
    {
        if (isInDialog == true && Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialog();
        }

        if (isInDialog == true && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
    public void StartDialog(Dialog dialog)
    {
        isInDialog = true;
        GameObject.FindObjectOfType<Movment>().enabled = false;
        playerRB.bodyType = RigidbodyType2D.Static;

        animator.SetBool("isOpen", true);

        nameText.text = dialog.name;
        avatarImage.sprite = dialog.avatar;

        sentences.Clear();

        foreach (string sentece in dialog.sentences)
        {
            sentences.Enqueue(sentece);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;

    }

    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        GameObject.FindObjectOfType<Movment>().enabled = true;
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        isInDialog = false;

        if (nameText.text == "Death Knight")
        {
            FindObjectOfType<levelManager>().endGame();
        }
    }

}
