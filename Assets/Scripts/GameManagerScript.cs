using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject corporate0;
    public GameObject corporateRemainingObject;
    public GameObject corporateDone;

    public Text corporateRemaining;
    public Text sprayRemaining;
    public Text score;

    public float numberSprayRemaining;
    public int scoreNumber;

    public int numberCorporate;

    public int numberCorporateSprayed;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    GameObject TweeLevens;
    GameObject EenLeven;

    private void Awake()
    {
        Cursor.visible = false;
        corporateRemainingObject.SetActive(false);
        corporateDone.SetActive(false);

        WinScreen = GameObject.FindGameObjectWithTag("WinScreen");
        WinScreen.SetActive(false);
        LoseScreen = GameObject.FindGameObjectWithTag("LoseScreen");
        LoseScreen.SetActive(false);

        TweeLevens = GameObject.FindGameObjectWithTag("2Levens");
        EenLeven = GameObject.FindGameObjectWithTag("1Leven");

        TweeLevens.SetActive(false);
        EenLeven.SetActive(false);
    }

    void Update()
    {
        corporateRemaining.text = numberCorporateSprayed + " / " + numberCorporate;

        if (numberCorporateSprayed == numberCorporate)
        {
            WinScreen.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().playerInputEnabled = false;
        }

        sprayRemaining.text = "Spray remaining: %" + Mathf.Round(numberSprayRemaining);

        score.text = "Score: " + scoreNumber;

        if (numberCorporateSprayed == numberCorporate)
        {
            corporateRemainingObject.SetActive(false);
            corporateDone.SetActive(true);
        }
        else if (numberCorporateSprayed > 0)
        {
            corporate0.SetActive(false);
            corporateRemainingObject.SetActive(true);
        }
    }

    public void Caught()
    {
        if (!TweeLevens.activeSelf)
        {
            TweeLevens.SetActive(true);
        }
        else if (!EenLeven.activeSelf)
        {
            EenLeven.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().playerInputEnabled = false;
            LoseScreen.SetActive(true);
            Cursor.visible = true;
        }
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSecondsRealtime(1);
    }
}
