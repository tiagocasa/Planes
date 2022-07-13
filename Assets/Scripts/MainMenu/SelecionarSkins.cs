using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecionarSkins : MonoBehaviour
{
    public Text skin1;
    public Text skin2;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("SkinSelecionada", 1) == 1)
        {
            skin1.text = "ATIVADO";
            skin2.text = "ATIVAR";
        }else if(PlayerPrefs.GetInt("SkinSelecionada", 1) == 2)
        {
            skin1.text = "ATIVAR";
            skin2.text = "ATIVADO";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skin1()
    {
        // Seta o menu principal pra desativo e ativa menu option

        FindObjectOfType<AudioManager>().Play("botao");
        PlayerPrefs.SetInt("SkinSelecionada", 1);
        skin1.text = "ATIVADO";
        skin2.text = "ATIVAR";
            // seta playerpref pra essa skin.
            //muda o texto pra ativado
        

    }

    public void Skin2()
    {
        FindObjectOfType<AudioManager>().Play("botao");
        PlayerPrefs.SetInt("SkinSelecionada", 2);
        skin1.text = "ATIVAR";
        skin2.text = "ATIVADO";
    }

}
