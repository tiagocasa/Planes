using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;

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


    private string username;
    public int totalCoins;
    private int highscore;
    private int cash;
    private int levelCoin;
    private int levelGas;
    private int levelMagnet;
    private int levelTurbo;

    private int[] chanceCoin = { 10, 20, 30, 50, 80 };
    private int[] quantityGas = { 15, 25, 50, 75, 100 };
    private int[] durationMagnet = { 10, 15, 20, 25, 30 };
    private int[] durationTurbo = { 5, 8, 10, 13, 15 };

    private string skinNameSelected;
    private bool hasSkin0;
    private bool hasSkin1;
    private bool hasSkin2;
    private bool hasSkin3;
    private Sprite skinSprite;

    public List<int> playerSkins = new List<int>();

    public string Username { get => username; set => username = value; }
    public int TotalCoins { get => totalCoins; set => totalCoins = value; }
    public int Highscore { get => highscore; set => highscore = value; }
    public int Cash { get => cash; set => cash = value; }
    public int LevelCoin { get => levelCoin; set => levelCoin = value; }
    public int LevelGas { get => levelGas; set => levelGas = value; }
    public int LevelMagnet { get => levelMagnet; set => levelMagnet = value; }
    public int LevelTurbo { get => levelTurbo; set => levelTurbo = value; }
    public bool HasSkin0 { get => hasSkin0; set => hasSkin0 = value; }
    public bool HasSkin1 { get => hasSkin1; set => hasSkin1 = value; }
    public bool HasSkin2 { get => hasSkin2; set => hasSkin2 = value; }
    public bool HasSkin3 { get => hasSkin3; set => hasSkin3 = value; }
    public int[] ChanceCoin { get => chanceCoin; set => chanceCoin = value; }
    public int[] QuantityGas { get => quantityGas; set => quantityGas = value; }
    public int[] DurationMagnet { get => durationMagnet; set => durationMagnet = value; }
    public int[] DurationTurbo { get => durationTurbo; set => durationTurbo = value; }
    public string SkinNameSelected { get => skinNameSelected; set => skinNameSelected = value; }
    public Sprite SkinSprite { get => skinSprite; set => skinSprite = value; }
    public List<int> PlayerSkins { get => playerSkins; set => playerSkins = value; }






    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
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
        PlayerSkins[0] = 0;
        SkinNameSelected = "Default";
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

    public void SetCoinLevel(string _level)
    {
        LevelCoin = int.Parse(_level);
    }

    public void SetGasLevel(string _level)
    {
        LevelGas = int.Parse(_level);
    }
    public void SetMagnetLevel(string _level)
    {
        LevelMagnet = int.Parse(_level);
    }
    public void SetTurboLevel(string _level)
    {
        LevelTurbo = int.Parse(_level);
    }

    public void SetUserName(string _username)
    {
        Username = _username;
    }

    public void SetSkinName(string _skinName)
    {
        SkinNameSelected = _skinName;
    }
    public void SetSkin0(string _hasSkin)
    {
        if (_hasSkin == "True")
        {
            hasSkin0 = true;
        }
    }
    public void SetSkin1(string _hasSkin)
    {
        if (_hasSkin == "True")
        {
            hasSkin1 = true;
        }
    }
    public void SetSkin2(string _hasSkin)
    {
        if (_hasSkin == "True")
        {
            hasSkin2 = true;
        }
    }
    public void SetSkin3(string _hasSkin)
    {
        if (_hasSkin == "True")
        {
            hasSkin3 = true;
        }
    }

    public void UpdateSkinList(DataSnapshot snapshot)
    {
        foreach (var child in snapshot.Children)
        {
            PlayerSkins.Add((int.Parse(child.Value.ToString())));
        }
    }    

}   
