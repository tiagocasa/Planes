using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ShopUp : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public string Nome;
        public string Descricao;
        public Sprite Image;
        public int Price;
        public int Highscore;
        public bool isPurchased = false;


    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator SemMoeda;
    [SerializeField] Animator SemHC;

    public Text coinText;
    public Text highScore;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;

    Button buyBtn;

    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("APARECER MOEDAS", 0) == 0)
        {
            PlayerPrefs.SetInt("APARECER MOEDAS", 1);
        }
        PlayerPrefs.SetInt("CHOPPER", 1);

        //Somente para testes
        //PlayerPrefs.SetInt("Coins", 100);
        //PlayerPrefs.SetInt("HighScore", 100);

        //PlayerPrefs que vamos utilizar
        //PlayerPrefs.GetInt("ShopMoeda",0);
        //PlayerPrefs.GetInt("ShopTurbo", 0);
        //PlayerPrefs.GetInt("ShopIma", 0);
        //PlayerPrefs.GetInt("ShopGasolina", 0);



        //Arumar Moedas e Highscore
        highScore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();



        // Criar Lista das Skins
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            ShopScrollView.GetChild(i + 1).GetChild(3).GetComponent<Text>().text = ShopItemsList[i].Descricao;
            // If para ver se ja tem essa skins, se sim aparecer texto Ativar"
            string nomeSkin = ShopItemsList[i].Nome;


            if (PlayerPrefs.GetInt(nomeSkin, 0) == 0)
            {
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + ShopItemsList[i].Highscore.ToString();
                g.transform.GetChild(4).GetComponent<Text>().text = "X0";
            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 1)
            {
                int preco = ShopItemsList[i].Price;
                preco *= 2;
                int hs = ShopItemsList[i].Highscore;
                hs *= 2;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                g.transform.GetChild(4).GetComponent<Text>().text = "X1";
            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 2)
            {
                int preco = ShopItemsList[i].Price;
                preco *= 3;
                int hs = ShopItemsList[i].Highscore;
                hs *= 3;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                g.transform.GetChild(4).GetComponent<Text>().text = "X2";
            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 3)
            {
                int preco = ShopItemsList[i].Price;
                preco *= 4;
                int hs = ShopItemsList[i].Highscore;
                hs *= 4;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                g.transform.GetChild(4).GetComponent<Text>().text = "X3";
            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 4)
            {
                int preco = ShopItemsList[i].Price;
                preco *= 4;
                int hs = ShopItemsList[i].Highscore;
                hs *= 4;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                g.transform.GetChild(4).GetComponent<Text>().text = "X4";
            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 5)
            {
                ShopScrollView.GetChild(i + 1).GetChild(1).gameObject.SetActive(false);
                ShopScrollView.GetChild(i + 1).GetChild(3).GetComponent<Text>().text = "TOTALMENTE MELHORADO";
                g.transform.GetChild(4).GetComponent<Text>().text = "X5";
            }


            buyBtn = g.transform.GetChild(5).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].isPurchased;
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            g.transform.GetChild(2).GetComponent<Text>().text = ShopItemsList[i].Nome;
        }

        Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        int coinsshop = PlayerPrefs.GetInt("Coins");
        int high = PlayerPrefs.GetInt("HighScore");
        int preco = ShopItemsList[itemIndex].Price;
        int hs = ShopItemsList[itemIndex].Highscore;
        string nomeSkin = ShopItemsList[itemIndex].Nome;
        Debug.Log(PlayerPrefs.GetInt(nomeSkin));

        //Verifica se ja tem a skin comprada
        if (PlayerPrefs.GetInt(nomeSkin) == 5)
        {
            FindObjectOfType<AudioManagerMenu>().Play("voltar");
            ShopItemsList[itemIndex].isPurchased = true;

        }
        else
        {
            //Verifica se tem moeda

            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(5).GetComponent<Button>();
            FindObjectOfType<AudioManagerMenu>().Play("botao");

            if (PlayerPrefs.GetInt(nomeSkin, 0) == 0)
            {
                if (coinsshop >= ShopItemsList[itemIndex].Price)
                {
                    if (high >= ShopItemsList[itemIndex].Highscore)
                    {
                        preco = ShopItemsList[itemIndex].Price;
                        coinsshop -= preco;
                        preco = ShopItemsList[itemIndex].Price * 2;
                        hs = ShopItemsList[itemIndex].Highscore * 2;
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Text>().text = "X1";

                        PlayerPrefs.SetInt(nomeSkin, 1);
                    }
                    else
                    {
                        SemHC.SetTrigger("SemHC");
                        FindObjectOfType<AudioManagerMenu>().Play("voltar");
                    }

                }
                else
                {
                    SemMoeda.SetTrigger("SemMoeda");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }
            }

            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 1)
            {
                if (coinsshop >= ShopItemsList[itemIndex].Price * 2)
                {
                    if (high >= ShopItemsList[itemIndex].Highscore * 2)
                    {
                        preco = ShopItemsList[itemIndex].Price * 2;
                        coinsshop -= preco;
                        preco = ShopItemsList[itemIndex].Price * 3;
                        hs = ShopItemsList[itemIndex].Highscore * 3;

                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Text>().text = "X2";
                        PlayerPrefs.SetInt(nomeSkin, 2);
                    }
                    else
                    {
                        SemHC.SetTrigger("SemHC");
                        FindObjectOfType<AudioManagerMenu>().Play("voltar");
                    }
                }
                else
                {
                    SemMoeda.SetTrigger("SemMoeda");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }

            }

            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 2)
            {
                if (coinsshop >= ShopItemsList[itemIndex].Price * 3)
                {
                    if (high >= ShopItemsList[itemIndex].Highscore * 3)
                    {
                        preco = ShopItemsList[itemIndex].Price * 3;
                        coinsshop -= preco;
                        preco = ShopItemsList[itemIndex].Price * 4;
                        hs = ShopItemsList[itemIndex].Highscore * 4;
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Text>().text = "X3";
                        PlayerPrefs.SetInt(nomeSkin, 3);
                    }
                    else
                    {
                        SemHC.SetTrigger("SemHC");
                        FindObjectOfType<AudioManagerMenu>().Play("voltar");
                    }
                }
                else
                {
                    SemMoeda.SetTrigger("SemMoeda");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }

            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 3)
            {
                if (coinsshop >= ShopItemsList[itemIndex].Price * 4)
                {
                    if (high >= ShopItemsList[itemIndex].Highscore * 4)
                    {
                        preco = ShopItemsList[itemIndex].Price * 4;
                        coinsshop -= preco;
                        preco = ShopItemsList[itemIndex].Price * 5;
                        hs = ShopItemsList[itemIndex].Highscore * 5;
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Text>().text = "X4";
                        PlayerPrefs.SetInt(nomeSkin, 4);
                    }
                    else
                    {
                        SemHC.SetTrigger("SemHC");
                        FindObjectOfType<AudioManagerMenu>().Play("voltar");
                    }
                }
                else
                {
                    SemMoeda.SetTrigger("SemMoeda");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }

            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 4)
            {
                if (coinsshop >= ShopItemsList[itemIndex].Price * 5)
                {
                    if (high >= ShopItemsList[itemIndex].Highscore * 5)
                    {
                        preco = ShopItemsList[itemIndex].Price * 5;
                        coinsshop -= preco;
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = preco.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(1).GetChild(1).GetComponent<Text>().text = "HIGHSCORE:" + hs.ToString();
                        ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Text>().text = "X5";
                        PlayerPrefs.SetInt(nomeSkin, 5);
                        ShopScrollView.GetChild(itemIndex).GetChild(1).gameObject.SetActive(false);
                        ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Text>().text = "TOTALMENTE MELHORADO";
                    }
                    else
                    {
                        SemHC.SetTrigger("SemHC");
                        FindObjectOfType<AudioManagerMenu>().Play("voltar");
                    }
                }
                else
                {
                    SemMoeda.SetTrigger("SemMoeda");
                    FindObjectOfType<AudioManagerMenu>().Play("voltar");
                }


            }
            else if (PlayerPrefs.GetInt(nomeSkin, 0) == 5)
            {

                FindObjectOfType<AudioManagerMenu>().Play("voltar");
            }
            PlayerPrefs.SetInt("Coins", coinsshop);
            coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();
        }
    }
}


