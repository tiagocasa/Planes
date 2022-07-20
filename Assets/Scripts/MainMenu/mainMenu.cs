using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] GameObject RankingContent;
    [SerializeField] Transform RankScrollView;
    [SerializeField] private GameObject meRanking;
    [SerializeField] private GameObject loading;

    public void Options()
    {
        // Seta o menu principal pra desativo e ativa menu option

        FindObjectOfType<AudioManager>().Play("botao");

    }
    public void ShowRank()
    {
        loading.SetActive(true);
        FindObjectOfType<AudioManager>().Play("botao");
        StartCoroutine(FindObjectOfType<FirebaseManager>().LoadScoreboardData(RankingContent,RankScrollView));
        if (!FindObjectOfType<FirebaseManager>().IsAnonimo())
        {
            meRanking.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
            meRanking.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<AvatarList>().spriteList[MenuManager.instance.AvatarId];
        }
        else
        {
            meRanking.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        meRanking.transform.GetChild(2).GetComponent<TMP_Text>().text = MenuManager.instance.Username;
        meRanking.transform.GetChild(3).GetComponent<TMP_Text>().text = "Highscore: " + MenuManager.instance.Highscore.ToString();

        
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
