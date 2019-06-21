using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Text corporateRemaining;
    public Text sprayRemaining;
    public Text score;

    public int numberCorporateRemaining;
    public float numberSprayRemaining;
    public int scoreNumber;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    private void Awake()
    {
        WinScreen = GameObject.FindGameObjectWithTag("WinScreen");
        WinScreen.SetActive(false);
        LoseScreen = GameObject.FindGameObjectWithTag("LoseScreen");
        LoseScreen.SetActive(false);
    }

    void Update()
    {
        corporateRemaining.text = "Number of corporate remaining: " + numberCorporateRemaining;

        if (numberCorporateRemaining <= 0)
        {
            WinScreen.SetActive(true);
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().playerInputEnabled = false;
            //SceneManager.LoadScene("level1");
        }

        sprayRemaining.text = "Spray remaining: %" + Mathf.Round(numberSprayRemaining);

        score.text = "Score: " + scoreNumber;
    }
}
