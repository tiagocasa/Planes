using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public int Highscore;
        public bool isPurchased = false;
        public Color NewColor;
        public string Nome;
        
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator SemMoeda;
    [SerializeField] Animator SemHC;

    public Text coinText;
    public Text highScore;
    string nomeSkin;
    public GameObject SkinSelec;

    public Sprite SkinM0;
    public Sprite SkinM1;
    public Sprite SkinM2;


    GameObject ItemTemplate;
    GameObject g;
    [SerializeField]Transform ShopScrollView;
  
    Button buyBtn;

    // Start is called before the first frame update
    void Start()
    {
        //Somente para testes
        PlayerPrefs.SetInt("CHOPPER", 1);
        //PlayerPrefs.SetInt("Coins", 500);
        //PlayerPrefs.SetInt("HighScore", 500);

        //Arumar Moedas e Highscore
        highScore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();

        if (PlayerPrefs.GetInt("SkinSelecionada") == 5)
        {
            SkinSelec.GetComponent<Image>().sprite = SkinM1;
        }
        else if (PlayerPrefs.GetInt("SkinSelecionada") == 6)
        {
            SkinSelec.GetComponent<Image>().sprite = SkinM2;
        }
        else
        {
            SkinSelec.GetComponent<Image>().sprite = SkinM0;
        }
        SkinSelec.GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("SkinSelecionadaRed", 255), PlayerPrefs.GetFloat("SkinSelecionadaGreen", 255), PlayerPrefs.GetFloat("SkinSelecionadaBlue", 255), 255);


        // Criar Lista das Skins
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;

            // If para ver se ja tem essa skins, se sim aparecer texto Ativar"
            nomeSkin = ShopItemsList[i].Nome;
            g.transform.GetChild(3).GetComponent<Text>().text = ShopItemsList[i].Nome;

            if (PlayerPrefs.GetInt(nomeSkin,0) == 0){ 
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + ShopItemsList[i].Highscore.ToString();
            }
            else
            {
                ShopScrollView.GetChild(i+1).GetChild(1).gameObject.SetActive(false);
                ShopScrollView.GetChild(i+1).GetChild(4).gameObject.SetActive(true);
                if (nomeSkin == "SKIN MISTERIOSA 1")
                {
                    g.transform.GetChild(0).GetComponent<Image>().sprite = SkinM1;
                    g.transform.GetChild(3).GetComponent<Text>().text = "DRAGÃO";
                }
                else if (nomeSkin == "SKIN MISTERIOSA 2")
                {
                    g.transform.GetChild(0).GetComponent<Image>().sprite = SkinM2;
                    g.transform.GetChild(3).GetComponent<Text>().text = "DEVAGAR E SEMPRE" +
                        "";
                }
            }

            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].isPurchased;
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            g.transform.GetChild(0).GetComponent<Image>().color = ShopItemsList[i].NewColor;
  
        }

        Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        int coinsshop = PlayerPrefs.GetInt("Coins");
        nomeSkin = ShopItemsList[itemIndex].Nome;
        int high = PlayerPrefs.GetInt("HighScore");

        //Verifica se ja tem a skin comprada
        if (PlayerPrefs.GetInt(nomeSkin)==1) {
            if (PlayerPrefs.GetInt("SkinSelecionada") == itemIndex) {
                FindObjectOfType<AudioManagerMenu>().Play("voltar");
            } else {
                
                PlayerPrefs.SetInt("SkinSelecionada", itemIndex);
                FindObjectOfType<AudioManagerMenu>().Play("botao");
                if (PlayerPrefs.GetInt("SkinSelecionada") == 5)
                {
                    SkinSelec.GetComponent<Image>().sprite = SkinM1;
                }
                else if (PlayerPrefs.GetInt("SkinSelecionada") == 6)
                {
                    SkinSelec.GetComponent<Image>().sprite = SkinM2;
                }
                else
                {
                    SkinSelec.GetComponent<Image>().sprite = SkinM0;
                }

                 SkinSelec.GetComponent<Image>().color = ShopItemsList[itemIndex].NewColor;
                PlayerPrefs.SetFloat("SkinSelecionadaRed", ShopItemsList[itemIndex].NewColor.r);
                PlayerPrefs.SetFloat("SkinSelecionadaBlue", ShopItemsList[itemIndex].NewColor.b);
                PlayerPrefs.SetFloat("SkinSelecionadaGreen", ShopItemsList[itemIndex].NewColor.g);
            }
        } else {
            //Verifica se tem moeda
            if (coinsshop >= ShopItemsList[itemIndex].Price) {
                if(high >= ShopItemsList[itemIndex].Highscore)
                {
                    ShopItemsList[itemIndex].isPurchased = true;
                    buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
                    //buyBtn.interactable = false;
                    ShopScrollView.GetChild(itemIndex).GetChild(1).gameObject.SetActive(false);
                    ShopScrollView.GetChild(itemIndex).GetChild(4).gameObject.SetActive(true);

                    // Mudar Sprite Skin
                    if (nomeSkin == "SKIN MISTERIOSA 1")
                    {
                        ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite = SkinM1;
                        ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Text>().text = "DRAGÃO";
                    }
                    else if (nomeSkin == "SKIN MISTERIOSA 2")
                    {
                        ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite = SkinM2;
                        ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Text>().text = "DEVAGAR E SEMPRE";
                    }

                    FindObjectOfType<AudioManagerMenu>().Play("botao");
                    PlayerPrefs.SetInt(nomeSkin, 1);
                    coinsshop -= ShopItemsList[itemIndex].Price;
                    PlayerPrefs.SetInt("Coins", coinsshop);
                    coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();
                }
                else
                {
                    SemHC.SetTrigger("SemHC");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }
               
            } else {
                SemMoeda.SetTrigger("SemMoeda");
                FindObjectOfType<AudioManagerMenu>().Play("voltar");
            }
        }
    }
    
}


