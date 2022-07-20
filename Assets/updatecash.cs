using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updatecash : MonoBehaviour
{
    [SerializeField] TMP_Text cash;
    [SerializeField] TMP_Text coin;
    [SerializeField] TMP_Text cash2;
    [SerializeField] TMP_Text coin2;

    // Update is called once per frame
    void Update()
    {
        cash.text = MenuManager.instance.Cash.ToString();
        cash2.text = MenuManager.instance.Cash.ToString();
        coin.text = MenuManager.instance.TotalCoins.ToString();
        coin2.text = MenuManager.instance.TotalCoins.ToString();



    }
}
