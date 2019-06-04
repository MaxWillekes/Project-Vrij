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

    void Update()
    {
        corporateRemaining.text = "Number of corporate remaining: " + numberCorporateRemaining;

        if (numberCorporateRemaining <= 0)
        {
            SceneManager.LoadScene("level1");
        }

        sprayRemaining.text = "Spray remaining: %" + Mathf.Round(numberSprayRemaining);

        score.text = "Score: " + scoreNumber;
    }
}
