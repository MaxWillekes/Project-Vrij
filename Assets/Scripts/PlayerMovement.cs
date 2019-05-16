﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float sensitivity = 1f;

    public GameObject spray;

    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;

    CharacterController characterController;

    private void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            //Multiply it by speed.
            moveDirection *= speed;

            //Jumping
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        turner = Input.GetAxis("Mouse X") * sensitivity;
        looker = -Input.GetAxis("Mouse Y") * sensitivity;

        if (turner != 0)
        {
            //Code for action on mouse moving right
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            //Code for action on mouse moving right
            transform.eulerAngles += new Vector3(looker, 0, 0);
        }

        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            spray.SetActive(true);
        }else if(Input.GetMouseButtonUp(0)){
            spray.SetActive(false);
        }
    }
}