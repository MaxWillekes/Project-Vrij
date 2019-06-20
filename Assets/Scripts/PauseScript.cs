using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

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
        Menu.SetActive(false);
        player.GetComponent<PlayerMovement>().playerInputEnabled = true;
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void mainMenu()
    {
        Application.LoadLevel("Menu");
        Time.timeScale = 1;
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1);
    }
}