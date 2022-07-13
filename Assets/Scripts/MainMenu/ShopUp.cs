using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private string novaDescricao;
    private string preco;
    private int nivel;

    private FirebaseManager fm;


    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator SemMoeda;

    GameObject ItemTemplate;
    GameObject shopContainer;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text coinText;

    Button buyBtn;

    private int[] coinChancePrice = { 10, 100, 500, 1000 };
    private int[] gasPrice = { 10, 100, 500, 1000 };
    private int[] magnetPrice = { 10, 100, 500, 1000 };
    private int[] turboPrice = { 10, 100, 500, 1000 };

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FirebaseManager>();
        cashText.text = MenuManager.instance.Cash.ToString();
        coinText.text = MenuManager.instance.TotalCoins.ToString();
        // Criar Lista das Skins
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            shopContainer = Instantiate(ItemTemplate, ShopScrollView);
            shopContainer.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;



            // If para ver se ja tem essa skins, se sim aparecer texto Ativar"
            string shopitem = ShopItemsList[i].Nome;

            if(shopitem == "Coin Spawn Chance")
            {
                if (MenuManager.instance.LevelCoin >= 4)
                {
                    novaDescricao = $"Chance of Spawning coins of {MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin]}%";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelCoin;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the spawn chance of coins to {MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin + 1]}%";
                    preco = coinChancePrice[MenuManager.instance.LevelCoin].ToString();
                    nivel = MenuManager.instance.LevelCoin;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                    buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin]}%"; // mudar pro valor
            }
            else if(shopitem == "Gasoline Refill")
            {
                // novaDescricao = $"Increase the ammount of gasoline recovered to {MenuManager.instance.quantityGas[MenuManager.instance.LevelGas + 1]}%";
                if (MenuManager.instance.LevelGas >= 4)
                {
                    novaDescricao = $"Increases the ammount of gasoline recovered to {MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas]}%";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelGas;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the ammount of gasoline recovered to {MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas + 1]}%";
                    preco = gasPrice[MenuManager.instance.LevelGas].ToString();
                    nivel = MenuManager.instance.LevelGas;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                    buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas]}%"; // mudar pro valor

            }
            else if (shopitem == "Magnet Duration")
            {
                // novaDescricao = $"Increase the duration of the Magnet effect to {MenuManager.instance.durationMagnet[MenuManager.instance.LevelMagnet + 1]} sec";
                if (MenuManager.instance.LevelMagnet >= 4)
                {
                    novaDescricao = $"Increases the duration of the magnet effect to {MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet]} sec";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelMagnet;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the duration of the magnet effect to {MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet + 1]} sec";
                    preco = magnetPrice[MenuManager.instance.LevelMagnet].ToString();
                    nivel = MenuManager.instance.LevelMagnet;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                    buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet]} sec"; // mudar pro valor
            }
            else if (shopitem == "Turbo Duration")
            {
                // novaDescricao = $"Increase the duration of the Turbo effet {MenuManager.instance.durationTurbo[MenuManager.instance.LevelTurbo + 1]} sec";
                if (MenuManager.instance.LevelTurbo >= 4)
                {
                    novaDescricao = $"Increases the duration of the turbo effect to {MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo]} sec";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelTurbo;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the duration of the turbo effect to {MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo + 1]} sec";
                    preco = coinChancePrice[MenuManager.instance.LevelTurbo].ToString();
                    nivel = MenuManager.instance.LevelTurbo;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                    buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo]} sec"; // mudar pro valor
            }
                       
            
            
            shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = novaDescricao;
            shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = preco;
            

           

            shopContainer.transform.GetChild(1).GetComponent<TMP_Text>().text = ShopItemsList[i].Nome;
        }

        Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        int coinsshop = MenuManager.instance.TotalCoins;
        int high = MenuManager.instance.Highscore;
        string nomeSkin = ShopItemsList[itemIndex].Nome;

        Debug.Log(PlayerPrefs.GetInt(nomeSkin));

        //Verifica se tem moeda
        shopContainer = ShopScrollView.GetChild(itemIndex).gameObject;
        buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();

        if (itemIndex == 0)
        {
            int precoLocal = coinChancePrice[MenuManager.instance.LevelCoin];

            if (coinsshop >= precoLocal)
            {
                FindObjectOfType<AudioManager>().Play("botao");
                MenuManager.instance.TotalCoins -= precoLocal;
                MenuManager.instance.LevelCoin++;
                if (MenuManager.instance.LevelCoin >= 4)
                {
                    novaDescricao = $"Chance of Spawning coins of {MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin]}%";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelCoin;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Increase the spawn chance of coins to {MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin + 1]}%";
                    preco = coinChancePrice[MenuManager.instance.LevelCoin].ToString();
                    nivel = MenuManager.instance.LevelCoin;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                    //buyBtn.AddEventListener(i, OnShopItemBtnClicked);
                }

                shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = novaDescricao;
                shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = preco;
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.ChanceCoin[MenuManager.instance.LevelCoin]}%"; // mudar pro valor

                fm.SaveDataButton();


            }
            else
            {
                SemMoeda.SetTrigger("SemMoeda");
                FindObjectOfType<AudioManager>().Play("voltar");
            }
        }
        else if (itemIndex == 1)
        {
            int precoLocal = gasPrice[MenuManager.instance.LevelGas];
            if (coinsshop >= precoLocal)
            {
                FindObjectOfType<AudioManager>().Play("botao");
                MenuManager.instance.TotalCoins -= precoLocal;
                MenuManager.instance.LevelGas++;
                if (MenuManager.instance.LevelGas >= 4)
                {
                    novaDescricao = $"Increases the ammount of gasoline recovered to {MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas]}%";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelGas;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increases the ammount of gasoline recovered to {MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas + 1]}%";
                    preco = coinChancePrice[MenuManager.instance.LevelGas].ToString();
                    nivel = MenuManager.instance.LevelGas;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                }

                shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = novaDescricao;
                shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = preco;
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas]}%"; // mudar pro valor

                fm.SaveDataButton();
            }
            else
            {
                SemMoeda.SetTrigger("SemMoeda");
                FindObjectOfType<AudioManager>().Play("voltar");
            }
        }
        else if(itemIndex == 2)
        {
            int precoLocal = magnetPrice[MenuManager.instance.LevelMagnet];
            if (coinsshop >= precoLocal)
            {
                FindObjectOfType<AudioManager>().Play("botao");
                MenuManager.instance.TotalCoins -= precoLocal;
                MenuManager.instance.LevelMagnet++;
                if (MenuManager.instance.LevelMagnet >= 4)
                {
                    novaDescricao = $"Increases the duration of the magnet effect by {MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet]}sec";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelMagnet;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the duration of the magnet effect by {MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet + 1]}sec";
                    preco = coinChancePrice[MenuManager.instance.LevelMagnet].ToString();
                    nivel = MenuManager.instance.LevelMagnet;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                }

                shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = novaDescricao;
                shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = preco;
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet]} sec"; // mudar pro valor

                fm.SaveDataButton();
            }
            else
            {
                SemMoeda.SetTrigger("SemMoeda");
                FindObjectOfType<AudioManager>().Play("voltar");
            }
        }
        else if(itemIndex == 3)
        {
            int precoLocal = turboPrice[MenuManager.instance.LevelTurbo];
            if (coinsshop >= precoLocal)
            {
                FindObjectOfType<AudioManager>().Play("botao");
                MenuManager.instance.TotalCoins -= precoLocal;
                MenuManager.instance.LevelTurbo++;
                if (MenuManager.instance.LevelTurbo >= 4)
                {
                    novaDescricao = $"Increases the duration of the magnet effect by {MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo]} sec";
                    preco = "MAX";
                    nivel = MenuManager.instance.LevelTurbo;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = false;
                }
                else
                {
                    novaDescricao = $"Upgrade to increase the duration of the magnet effect by {MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo + 1]} sec";
                    preco = coinChancePrice[MenuManager.instance.LevelTurbo].ToString();
                    nivel = MenuManager.instance.LevelTurbo;
                    buyBtn = shopContainer.transform.GetChild(2).GetComponent<Button>();
                    buyBtn.interactable = true;
                }

                shopContainer.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = novaDescricao;
                shopContainer.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = preco;
                shopContainer.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"{MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo]} sec"; // mudar pro valor

                fm.SaveDataButton();
            }
            else
            {
                SemMoeda.SetTrigger("SemMoeda");
                FindObjectOfType<AudioManager>().Play("voltar");
            }
        }

        cashText.text = MenuManager.instance.Cash.ToString();
        coinText.text = MenuManager.instance.TotalCoins.ToString();



        ////Update Coins
        //PlayerPrefs.SetInt("Coins", coinsshop);
        //coinText.text = "MOEDAS:" + PlayerPrefs.GetInt("Coins", 0).ToString();

    }
}


