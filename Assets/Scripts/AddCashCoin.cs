using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCashCoin : MonoBehaviour
{
    [SerializeField] private FirebaseManager fm;
    [SerializeField] private GameObject coinTab;
    [SerializeField] private GameObject registerTab;


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


    public void BuyCoin()
    {
        StartCoroutine(fm.LoadUserData(BuyCoins2));
    }

    public void BuyCoins2()
    {
        MenuManager.instance.TotalCoins += 100;
        MenuManager.instance.Cash -= 10;
        fm.SaveDataButton();
    }
}
