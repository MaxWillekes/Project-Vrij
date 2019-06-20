using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Retry()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
