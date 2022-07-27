using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public TMP_Text number;
    public float timer;
    public TMP_Text cash;

    private void Start()
    {
        timer = 0;
        number.text = "5";
        cash.text = MenuManager.instance.Cash.ToString();
        FindObjectOfType<AudioManager>().Play("beep");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            number.text = "0";
            FindObjectOfType<AudioManager>().Stop("beep");
            this.gameObject.SetActive(false);
            FindObjectOfType<GameControl>().GameOverScreen();
        }
        else if (timer > 4)
        {
            number.text = "1";
        }
        else if (timer > 3)
        {
            number.text = "2";
        }
        else if (timer > 2)
        {
            number.text = "3";
        }
        else if (timer > 1)
        {
            number.text = "4";
        }
    }

    public void ContinueButton()
    {
        if (MenuManager.instance.Cash >= 10)
        {
            FindObjectOfType<AudioManager>().Stop("beep");
            MenuManager.instance.Cash -= 10;
            FindObjectOfType<GameControl>().ContinueBtn();
            timer = -60;
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateCash(MenuManager.instance.Cash));
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("voltar");
        }
        
    }
}
