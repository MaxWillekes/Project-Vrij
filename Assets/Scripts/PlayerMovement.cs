using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float sensitivity = 1f;

    public float sprayRemaining = 100f;

    public GameObject spray;
    public GameObject playerTagHolder;

    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;

    CharacterController characterController;

    public bool playerInputEnabled = true;

    void Start()
    {
        playerTagHolder = GameObject.FindGameObjectWithTag("PlayerTagHolder");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().numberSprayRemaining = 100f;
    }

    private void Update()
    {
        if (playerInputEnabled) {
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

            if (Input.GetMouseButton(0) && sprayRemaining >= 0.1f)
            {
                sprayRemaining -= 10 * Time.deltaTime;
                spray.SetActive(true);
            }
            else if (!Input.GetMouseButton(0) || sprayRemaining <= 0.5f)
            {
                spray.SetActive(false);
            }

            if (Input.GetKeyDown("left shift"))
            {
                speed += 6f;
            } else if (Input.GetKeyUp("left shift"))
            {
                speed -= 6f;
            }

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().numberSprayRemaining = sprayRemaining;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Refil")
        {
            Debug.LogWarning("Refil");
            sprayRemaining = 100;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TagAbleWall" && other.GetComponent<SpriteRenderer>().color.a > 1)
        {
            if (Input.GetKeyDown("e"))
            {
                other.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.tag = "Untagged";
                playerTagHolder.tag = "Player";
                gameObject.SetActive(false);
            }
        }
    }
}