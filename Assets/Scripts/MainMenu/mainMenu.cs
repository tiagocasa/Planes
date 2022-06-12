using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    
  

    public void Options()
    {
        // Seta o menu principal pra desativo e ativa menu option

        FindObjectOfType<AudioManagerMenu>().Play("botao");

    }


    public void QuitGame()
    {
        FindObjectOfType<AudioManagerMenu>().Play("botao");
        Application.Quit();

    }

    public void VoltarSound()
    {
        FindObjectOfType < AudioManagerMenu>().Play("voltar");

    }


}
