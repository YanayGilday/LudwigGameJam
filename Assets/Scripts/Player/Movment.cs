using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment: MonoBehaviour
{
    public CharacterController2D controller;

    private float x;

    public float runSpeed;
    bool jump = false;
    bool crouch = false;
    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

    }

    private void FixedUpdate()
    {
        controller.Move(x * Time.fixedDeltaTime, false , jump);
        jump = false;
    }






}
