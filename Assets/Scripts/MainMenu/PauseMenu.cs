using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameControl.instance.gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameControl.instance.gamePaused = false;
        FindObjectOfType<AudioManager>().Play("Helicoptero");
        FindObjectOfType<AudioManager>().Play("voltar");
    }

    public void Pause()
    {   
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameControl.instance.gamePaused = true;
        FindObjectOfType<AudioManager>().Play("botao");
        FindObjectOfType<AudioManager>().Stop("Helicoptero");
    }
}   
