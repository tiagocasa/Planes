using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCashCoin : MonoBehaviour
{
    [SerializeField] private FirebaseManager fm;
    [SerializeField] private GameObject coinTab;
    [SerializeField] private GameObject registerTab;
    [SerializeField] private GameObject loading;


    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            coinTab = FindObjectOfType<NewMenu>().transform.GetChild(7).gameObject;
            fm = FindObjectOfType<FirebaseManager>();
        }
    }

    public void AddCash()
    {
        if (fm.IsAnonimo())
        {
            registerTab.SetActive(true);
        }
        else
        {
            coinTab.SetActive(true);
            coinTab.transform.GetChild(0).gameObject.SetActive(true);
            coinTab.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void AddCoin()
    {
        if (fm.IsAnonimo())
        {
            registerTab.SetActive(true);
        }
        else
        {
            coinTab.SetActive(true);
            coinTab.transform.GetChild(0).gameObject.SetActive(false);
            coinTab.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    public void BuyCoin1()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCoins1Value));
    }

    public void BuyCoins1Value()
    {
        if (MenuManager.instance.Cash > 40)
        {
            FindObjectOfType<AudioManager>().Play("botao");
            MenuManager.instance.TotalCoins += 100;
            MenuManager.instance.Cash -= 40;
            fm.SaveDataButton();
            loading.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("voltar");
        }

    }

    public void BuyCoin2()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCoins2Value));
    }

    public void BuyCoins2Value()
    {
        if (MenuManager.instance.Cash > 200)
        {
            FindObjectOfType<AudioManager>().Play("botao");
            MenuManager.instance.TotalCoins += 525;
        MenuManager.instance.Cash -= 200;
        fm.SaveDataButton();
        loading.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("voltar");
        }
    }

    public void BuyCoin3()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCoins3Value));
    }

    public void BuyCoins3Value()
    {
        if (MenuManager.instance.Cash > 400)
        {
            FindObjectOfType<AudioManager>().Play("botao");
            MenuManager.instance.TotalCoins += 1155;
        MenuManager.instance.Cash -= 400;
        fm.SaveDataButton();
        loading.SetActive(false);
    }
        else
        {
            FindObjectOfType<AudioManager>().Play("voltar");
}
    }

    public void BuyCoin4()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCoins4Value));
    }

    public void BuyCoins4Value()
    {
        if (MenuManager.instance.Cash > 2000)
        {
        FindObjectOfType<AudioManager>().Play("botao");
        MenuManager.instance.TotalCoins += 6930;
        MenuManager.instance.Cash -= 2000;
        fm.SaveDataButton();
        loading.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("voltar");
        }
    }













    public void BuyCash1()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCash1Value));
    }
    
    private void BuyCash1Value()
    {
        MenuManager.instance.Cash += 100;
        fm.SaveDataButton();
        loading.SetActive(false);
    }

    public void BuyCash2()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCash2Value));
    }

    private void BuyCash2Value()
    {
        MenuManager.instance.Cash += 525;
        fm.SaveDataButton();
        loading.SetActive(false);
    }

    public void BuyCash3()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCash3Value));
    }

    private void BuyCash3Value()
    {
        MenuManager.instance.Cash += 1150;
        fm.SaveDataButton();
        loading.SetActive(false);
    }

    public void BuyCash4()
    {
        loading.SetActive(true);
        StartCoroutine(fm.LoadUserData(BuyCash4Value));
    }

    private void BuyCash4Value()
    {
        MenuManager.instance.Cash += 6000;
        fm.SaveDataButton();
        loading.SetActive(false);
    }







}
