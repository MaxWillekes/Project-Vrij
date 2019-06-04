using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafittiScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerTagHolder;

    private void Awake()
    {
        playerTagHolder = GameObject.FindGameObjectWithTag("PlayerTagHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("e"))
        {
            player.SetActive(true);
            player.tag = "Player";
            playerTagHolder.tag = "PlayerTagHolder";
            gameObject.SetActive(false);
        }
    }
}