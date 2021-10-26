using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public bool followY;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (followY == true)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        }
        
    }
}

