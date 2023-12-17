using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject HowToPlayScene;

    public void Awake()
    {
        HowToPlayScene.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        HowToPlayScene.SetActive(true);
    }

    public void Back()
    {
        HowToPlayScene.SetActive(false);
    }
}
