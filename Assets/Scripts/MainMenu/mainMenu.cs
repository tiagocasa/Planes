using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{


    public void Options()
    {
        // Seta o menu principal pra desativo e ativa menu option

        FindObjectOfType<AudioManager>().Play("botao");

    }


    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("botao");
        Application.Quit();
    }

    public void VoltarSound()
    {
        FindObjectOfType < AudioManager>().Play("voltar");
    }


}
