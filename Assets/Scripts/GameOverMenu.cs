using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverMenu : MonoBehaviour
{
    public bool enablecont;
    public Button yourbutton;
    public Animator continueanim;
    public AdManager Adman;
   
    void Awake()
    {
        
        if (PlayerPrefs.GetInt("Continue", 0) == 1)
        {
            enablecont = true;
        }
        else
        {
            enablecont = false;
        }
    }

    public void Continue()
    {
        Adman.PlayInterAD();

        //if (PlayerPrefs.GetInt("Continue", 0) == 1)
        //{
        //    enablecont = true;
        //}
        //else
        //{
        //    enablecont = false;
        //}

        //if (enablecont == true)
        //{
            
        //    //yourbutton.interactable = false;
        //    PlayerPrefs.SetInt("Continue", 0);
        //    continueanim.SetTrigger("ContinueAn");
        //    FindObjectOfType<AudioManager>().Play("voltar");
        //}

        //if (enablecont == false)
        //{
        //    yourbutton.interactable = true;
        //    PlayerPrefs.SetInt("Continue", 1);
        //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        //}


    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
