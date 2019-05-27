using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Text corporateRemaining;
    public int numberCorporateRemaining;

    // Update is called once per frame
    void Update()
    {
        corporateRemaining.text = "Number of corporate remaining: " + numberCorporateRemaining;

        if (numberCorporateRemaining <= 0)
        {
            Debug.Log("Win");
        }
    }
}
