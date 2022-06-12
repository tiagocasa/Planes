using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrades : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public int Highscore;
        public bool isPurchased = false;
        public string Nome;
        public string Multiplicador;
        
        
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator SemMoeda;
    [SerializeField] Animator SemHC;

    public Text coinText;
    public Text highScore;
    string nomeItem;
    public GameObject SkinSelec;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField]Transform ShopScrollView;
  
    Button buyBtn;

    // Start is called before the first frame update
    void Start()
    {
        //Somente para testes
        PlayerPrefs.SetInt("CHOPPER", 1);
        PlayerPrefs.SetInt("Coins", 100);
        PlayerPrefs.SetInt("HighScore", 100);

        //Arumar Moedas e Highscore
        highScore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();
        SkinSelec.GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("SkinSelecionadaRed", 255), PlayerPrefs.GetFloat("SkinSelecionadaGreen", 255), PlayerPrefs.GetFloat("SkinSelecionadaBlue", 255), 255);


        // Criar Lista das Skins
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;

            // If para ver se ja tem essa skins, se sim aparecer texto Ativar"
            nomeItem = ShopItemsList[i].Nome;

            // Primeira ideia, definir "nome do item " como playerprefs 1, 2,3,4 5 e fazer um if de acordo.

            if (PlayerPrefs.GetInt(nomeItem,0) == 0){ 
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + ShopItemsList[i].Highscore.ToString();
            }
            else
            {
                ShopScrollView.GetChild(i+1).GetChild(1).gameObject.SetActive(false);
                ShopScrollView.GetChild(i+1).GetChild(4).gameObject.SetActive(true);
            }

            //buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            //buyBtn.interactable = !ShopItemsList[i].isPurchased;
            //buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            g.transform.GetChild(3).GetComponent<Text>().text = ShopItemsList[i].Nome;
            g.transform.GetChild(5).GetComponent<Text>().text = ShopItemsList[i].Multiplicador;
        }

        Destroy(ItemTemplate);
    }

    //void OnShopItemBtnClicked(int itemIndex)
    //{
    //    int coinsshop = PlayerPrefs.GetInt("Coins");
    //    nomeSkin = ShopItemsList[itemIndex].Nome;
    //    int high = PlayerPrefs.GetInt("HighScore");

    //    //Verifica se ja tem a skin comprada
    //    if (PlayerPrefs.GetInt(nomeSkin)==1) {
    //        if (PlayerPrefs.GetInt("SkinSelecionada") == itemIndex) {
    //            FindObjectOfType<AudioManagerMenu>().Play("voltar");
    //        } else {
    //            PlayerPrefs.SetInt("SkinSelecionada", itemIndex);
    //            FindObjectOfType<AudioManagerMenu>().Play("botao");
    //            SkinSelec.GetComponent<Image>().color = ShopItemsList[itemIndex].NewColor;
    //            PlayerPrefs.SetFloat("SkinSelecionadaRed", ShopItemsList[itemIndex].NewColor.r);
    //            PlayerPrefs.SetFloat("SkinSelecionadaBlue", ShopItemsList[itemIndex].NewColor.b);
    //            PlayerPrefs.SetFloat("SkinSelecionadaGreen", ShopItemsList[itemIndex].NewColor.g);
    //        }
    //    } else {
    //        //Verifica se tem moeda
    //        if (coinsshop >= ShopItemsList[itemIndex].Price) {
    //            if(high >= ShopItemsList[itemIndex].Highscore)
    //            {
    //                ShopItemsList[itemIndex].isPurchased = true;
    //                buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
    //                //buyBtn.interactable = false;
    //                ShopScrollView.GetChild(itemIndex).GetChild(1).gameObject.SetActive(false);
    //                ShopScrollView.GetChild(itemIndex).GetChild(4).gameObject.SetActive(true);
    //                FindObjectOfType<AudioManagerMenu>().Play("botao");
    //                PlayerPrefs.SetInt(nomeSkin, 1);
    //                coinsshop -= ShopItemsList[itemIndex].Price;
    //                PlayerPrefs.SetInt("Coins", coinsshop);
    //                coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();
    //            }
    //            else
    //            {
    //                SemHC.SetTrigger("SemHC");
    //                FindObjectOfType<AudioManagerMenu>().Play("voltar");
    //            }
               
    //        } else {
    //            SemMoeda.SetTrigger("SemMoeda");
    //            FindObjectOfType<AudioManagerMenu>().Play("voltar");
    //        }
    //    }
    //}
    
}


