using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Text corporateRemaining;
    public Text sprayRemaining;

    public int numberCorporateRemaining;
    public float numberSprayRemaining;

    void Update()
    {
        corporateRemaining.text = "Number of corporate remaining: " + numberCorporateRemaining;

        if (numberCorporateRemaining <= 0)
        {
            Debug.Log("Win");
        }

        sprayRemaining.text = "Spray remaining: %" + Mathf.Round(numberSprayRemaining);
    }
}
