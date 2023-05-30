using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMenager : MonoBehaviour
{
    public void OnStartScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnBackinMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnLoadGameScene()
    {
        SceneManager.LoadScene(this.gameObject.name.Replace(' ', '_'));
    }
}
