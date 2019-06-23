using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    //button sprite switches//
    public Sprite OffSpriteRetry;
    public Sprite OnSpriteRetry;
    public Button retrybut;

    public Sprite offSpriteHome;
    public Sprite onSpriteHome;
    public Button homebut;

    public Sprite offSpriteResume;
    public Sprite onSpriteResume;
    public Button resumeButton;


    public GameObject Menu;
    GameObject player;

    public void Start () {
        Menu.SetActive(false);
        Time.timeScale = 1;

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	public void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().playerInputEnabled = false;
            Menu.SetActive(true);
        }
    }

    public void Resume()
    {
        if (resumeButton.image.sprite == onSpriteResume)
        {
            resumeButton.image.sprite = offSpriteResume;
        }
        else
        {
            resumeButton.image.sprite = onSpriteResume;
        }

        //SceneManager.LoadScene("Menu");//
        Time.timeScale = 1;
        Menu.SetActive(false);
        player.GetComponent<PlayerMovement>().playerInputEnabled = true;
        Time.timeScale = 1;
    }

    public void Retry()
    {
        if (retrybut.image.sprite == OnSpriteRetry)
        {
            retrybut.image.sprite = OffSpriteRetry;
        }
        else
        {
            retrybut.image.sprite = OnSpriteRetry;
        }

        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void mainMenu()
    {
        if (homebut.image.sprite == onSpriteHome)
        {
            homebut.image.sprite = offSpriteHome;
        }
        else
        {
            homebut.image.sprite = onSpriteHome;
        }
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1);
    }
}