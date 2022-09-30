using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pauseCanvas;
    public void PauseHandler()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
