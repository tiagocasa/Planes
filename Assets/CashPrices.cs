using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashPrices : MonoBehaviour
{
    [SerializeField] TMP_Text but1;
    [SerializeField] TMP_Text but2;

    [SerializeField] TMP_Text but3;
    [SerializeField] TMP_Text but4;
    void Start()
    {

    }

    private void OnEnable()
    {
        but1.text = MenuManager.instance.btn1;
        but2.text = MenuManager.instance.btn2;
        but3.text = MenuManager.instance.btn3;
        but4.text = MenuManager.instance.btn4;
    }

}
