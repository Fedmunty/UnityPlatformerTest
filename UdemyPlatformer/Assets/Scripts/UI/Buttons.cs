using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ContinueHandler()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartHandler()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }
    public void MainMenuHandler()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGameHandler()
    {
        Application.Quit();
    }
    public void StartHandler()
    {
        SceneManager.LoadScene(1);
    }

}
