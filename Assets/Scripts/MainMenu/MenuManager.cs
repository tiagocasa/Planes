using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Telas")]
    public GameObject SignupScreen;
    public GameObject SettingsScreen;

    [Header("Texto para atualizar")]
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text coinText;



    private int totalCoins;
    private int highscore;
    private int cash;


    public int TotalCoins { get => totalCoins; set => totalCoins = value; }
    public int Highscore { get => highscore; set => highscore = value; }
    public int Cash { get => cash; set => cash = value; }


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccountCreations()
    {
        TotalCoins = 0;
        Highscore = 0;
        Cash = 0;
    }


    public void ScreenUpdate()
    {
        highscoreText.text = Highscore.ToString();
        coinText.text = TotalCoins.ToString();
        cashText.text = Cash.ToString();
    }



    public void SetHighscore(string _score)
    {
        Highscore = int.Parse(_score);
    }
    public void SetCoin(string _coins)
    {
        TotalCoins = int.Parse(_coins);
    }
    public void SetCash(string _cash)
    {
        Cash = int.Parse(_cash);
    }

}
