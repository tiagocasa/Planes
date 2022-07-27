using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public GameObject Screen;
    public TMP_Text gold;
    public TMP_Text cash;
    private int goldAmount;
    private int cashAmount;
    public Sprite[] boxSprite;

    private void Start()
    {
        goldAmount = UnityEngine.Random.Range(1, 10);
        cashAmount = UnityEngine.Random.Range(1, 5);
        gold.text = goldAmount.ToString();
        cash.text = cashAmount.ToString();
        FindObjectOfType<AudioManager>().Play("positive");
        if(goldAmount+cashAmount == 15)
        {
            this.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = boxSprite[0];
            this.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "DIAMOND LUCKY BOX";
        }
        else if (goldAmount + cashAmount >= 12)
        {
            this.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = boxSprite[1];
            this.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "PLATINUM LUCKY BOX";
        }
        else if (goldAmount + cashAmount >= 8)
        {
            this.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = boxSprite[2];
            this.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "GOLD LUCKY BOX";
        }
        else if (goldAmount + cashAmount >= 4)
        {
            this.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = boxSprite[3];
            this.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "SILVER LUCKY BOX";
        }
        else
        {
            this.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = boxSprite[4];
            this.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "BRONZE LUCKY BOX";
        }
    }
    public void GetReward()
    {
        MenuManager.instance.Cash += cashAmount;
        MenuManager.instance.TotalCoins += goldAmount;
        FindObjectOfType<Notifications>().SendNotification("Your daily reward is available", "Come back to get it now!");
        StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateCash(MenuManager.instance.Cash));
        StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateCoin(MenuManager.instance.TotalCoins));
        FindObjectOfType<NewMenu>().ScreenUpdate();
        Screen.SetActive(false);
    }
}
