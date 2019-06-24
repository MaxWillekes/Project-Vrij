using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public CharacterController characterController;

    public Animator animatorChar;

    public bool playerInputEnabled = true;

    public Image grafFill;

    public Image emptyCan;

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
                animatorChar.SetBool("IsJumping", false);
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);


                //Multiply it by speed.
                moveDirection *= speed;

                if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") ||Input.GetKey("s") )
                {
                    animatorChar.SetBool("IsRunning", true);
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    animatorChar.SetBool("IsRunning", false);
                    GetComponent<AudioSource>().Stop();
                }



                //Jumping
                if (Input.GetButton("Jump"))
                {
                    
                    moveDirection.y = jumpSpeed;
                    animatorChar.SetBool("IsJumping", true);
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
                grafFill.fillAmount -= 0.1f *Time.deltaTime;
                spray.SetActive(true);
                if (!spray.GetComponent<AudioSource>().isPlaying)
                {
                    spray.GetComponent<AudioSource>().Play();
                }
                animatorChar.SetBool("IsSpraying", true);

            }
            else if (!Input.GetMouseButton(0) || sprayRemaining <= 0.5f)
            {
                spray.SetActive(false);
                animatorChar.SetBool("IsSpraying", false);
                spray.GetComponent<AudioSource>().Stop();
            }

            if( sprayRemaining == 100)
            {
                grafFill.fillAmount = 1;
            }
            else if (sprayRemaining == 0)
            {
                grafFill.fillAmount = 0;
            }

            if ( sprayRemaining <= 0.5f){
                emptyCan.enabled = true;
            }
            else if (sprayRemaining >= 0.1f)
            {
                emptyCan.enabled = false;
            }

            if (Input.GetKeyDown("left shift"))
            {
                speed += 3f;
            } else if (Input.GetKeyUp("left shift"))
            {
                speed -= 3f;
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