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

    public string btn1;
    public string btn2;
    public string btn3;
    public string btn4;


   public string username;
    private int avatarId;
    public int totalCoins;
    private int highscore;
    private int cash;
    private int levelCoin;
    private int levelGas;
    private int levelMagnet;
    private int levelTurbo;

    private int[] chanceCoin = { 20, 35,50, 70, 90 };
    private int[] quantityGas = { 15, 25, 50, 75, 100 };
    private int[] durationMagnet = { 20, 25, 30, 40, 50 };
    private int[] durationTurbo = { 5, 8, 10, 15, 20 };

    private string skinNameSelected;
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
    public int[] ChanceCoin { get => chanceCoin; set => chanceCoin = value; }
    public int[] QuantityGas { get => quantityGas; set => quantityGas = value; }
    public int[] DurationMagnet { get => durationMagnet; set => durationMagnet = value; }
    public int[] DurationTurbo { get => durationTurbo; set => durationTurbo = value; }
    public string SkinNameSelected { get => skinNameSelected; set => skinNameSelected = value; }
    public Sprite SkinSprite { get => skinSprite; set => skinSprite = value; }
    public List<int> PlayerSkins { get => playerSkins; set => playerSkins = value; }
    public int AvatarId { get => avatarId; set => avatarId = value; }






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
        Username = "Guest";
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

    public void SetAvatarId(string _avatarId)
    {
        AvatarId = int.Parse(_avatarId);
    }

    public void UpdateSkinList(DataSnapshot snapshot)
    {
        foreach (var child in snapshot.Children)
        {
            PlayerSkins.Add((int.Parse(child.Value.ToString())));
        }
    }    

}   
