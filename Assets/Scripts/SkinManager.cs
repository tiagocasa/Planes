using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [System.Serializable]
    class Skins
    {
        public string Nome;
        public Sprite Image;
        public Sprite GameSprite;
        public string Descricao;
        public Currency currency;
        public int price;
    }

    public enum Currency
    {
       Cash,
       Coin
    }
  

    [SerializeField] List<Skins> SkinList;

    private FirebaseManager fm;
    [SerializeField] Animator SemMoeda;

    GameObject ItemTemplate;
    GameObject shopContainer;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text coinText;

    Button buyBtn;

    private void Start()
    {

            if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
            fm = FindObjectOfType<FirebaseManager>();
            cashText.text = MenuManager.instance.Cash.ToString();
            coinText.text = MenuManager.instance.TotalCoins.ToString();

            // Criar Lista das Skins

            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            int len = SkinList.Count;

            

            for (int i = 0; i < len; i++)
            {
                shopContainer = Instantiate(ItemTemplate, ShopScrollView);
                shopContainer.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = SkinList[i].Image;
                shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = SkinList[i].Descricao;
                shopContainer.transform.GetChild(1).GetComponent<TMP_Text>().text = SkinList[i].Nome;

   
                if (MenuManager.instance.PlayerSkins.Contains(i)) 
                {
                    shopContainer.transform.GetChild(2).gameObject.SetActive(false);
                    shopContainer.transform.GetChild(3).gameObject.SetActive(true);

                    buyBtn = shopContainer.transform.GetChild(3).GetComponent<Button>();
                    buyBtn.AddEventListener(i, OnSkinPurchasedBtnClicked);
                    if (MenuManager.instance.SkinNameSelected == SkinList[i].Nome)
                    {
                        buyBtn.interactable = false;
                        shopContainer.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                        shopContainer.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                        
                    }
                    else
                    {
                        shopContainer.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                        shopContainer.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                        buyBtn.interactable = true;
                        
                    }
                    

                } 
                else
                {
                    shopContainer.transform.GetChild(2).gameObject.SetActive(true);
                    shopContainer.transform.GetChild(3).gameObject.SetActive(false);

                    if (SkinList[i].currency == Currency.Cash)
                    {
                        shopContainer.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                        shopContainer.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                        shopContainer.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                        shopContainer.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
                        shopContainer.transform.GetChild(2).GetChild(3).GetComponent<TMP_Text>().text = SkinList[i].price.ToString();
                    }
                    else
                    {
                        shopContainer.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                        shopContainer.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                        shopContainer.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                        shopContainer.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
                        shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = SkinList[i].price.ToString();

                    }

                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }
            }

            Destroy(ItemTemplate);
        }
    }


    void OnShopItemBtnClicked(int itemIndex)
    {
        int coinsshop = MenuManager.instance.TotalCoins;
        int cash = MenuManager.instance.Cash;
        string nomeSkin = SkinList[itemIndex].Nome;

        Debug.Log(PlayerPrefs.GetInt(nomeSkin));

        shopContainer = ShopScrollView.GetChild(itemIndex).gameObject;
        buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
        
        int len = SkinList.Count;
        for (int i = 0; i < len; i++)
        {
            if (i == itemIndex)
            {
                int precoLocal = SkinList[i].price;
                if ((SkinList[i].currency == Currency.Cash && cash > precoLocal) || (SkinList[i].currency == Currency.Coin && coinsshop > precoLocal))
                {
                    shopContainer.transform.GetChild(2).gameObject.SetActive(false);
                    shopContainer.transform.GetChild(3).gameObject.SetActive(true);
                    shopContainer.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                    shopContainer.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                    buyBtn = shopContainer.transform.GetChild(3).GetComponent<Button>();
                    buyBtn.interactable = true;
                    buyBtn.AddEventListener(i, OnSkinPurchasedBtnClicked);
                    StartCoroutine(fm.UpdateSkinList(i));
                }


            }
        }
        
        
    }

    void OnSkinPurchasedBtnClicked(int itemIndex)
    {
        int len = SkinList.Count;
        for (int i = 0; i < len; i++)
        {
            GameObject shop = ShopScrollView.transform.GetChild(i).gameObject;
            buyBtn = shop.transform.GetChild(3).GetComponent<Button>();
            if (i == itemIndex)
            {
                shop.transform.GetChild(2).gameObject.SetActive(false);
                shop.transform.GetChild(3).gameObject.SetActive(true);

                
                buyBtn.interactable = false;
                shop.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                shop.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                MenuManager.instance.SkinNameSelected = SkinList[i].Nome;
                fm.UpdateSelectSkinName(SkinList[i].Nome);
            }
            else
            {
                if (MenuManager.instance.PlayerSkins.Contains(i))
                {
                    shop.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                    shop.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                    buyBtn.interactable = true;
                }
                
            }
        }
    }

    public Sprite GetSkin(string _skinName)
    {
        int len = SkinList.Count;

        for (int i = 0; i < len; i++)
        {
            if(SkinList[i].Nome == _skinName)
            {
                Debug.Log(SkinList[i].GameSprite);

                return SkinList[i].GameSprite;
                
            }
        }
        Debug.Log("skin null");
        return null;
    }
}
