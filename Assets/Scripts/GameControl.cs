using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using System.Media;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    private FirebaseManager manager;
    public GameObject coinEffect;


    public GameObject gameOverText;
    public GameObject gameHUD;
    public bool gameOver = false;
    public bool gamePaused = false;


    public float gasoline = 100f;
    public Slider gasolineSlider;

    public float scrollSpeed = -1.5f;
    public float incrementSpeed = 2f;
    public float tempSpeed;


    public Text scoreText;
    public GameObject highScore;

    public int score = 0;



    [SerializeField] private int coins = 0;
    private int totalCoins;


    public float backgroundTimer;
    public int orderMap;
    public int groundIndex;
    public bool transitionGround;

    public int foregroundIndex;
    public bool transitionForeground;


    public int skyIndex;
    public bool transitionSky;


    public TMP_Text coinText;
    public TMP_Text coinGO;
    public TMP_Text highscoretext;
    public TMP_Text highscoreGO;

    public bool isDash = false;
    public float timedash=0;

    public bool isIma = false;
    private float timeIma = 0;

    private float TempoIma;
    private float TempoDash;

    public float turboFuel;
    public Slider turboSlider;

    public GameObject Continue;
    private bool isContinue;
    private bool alreadyContinue;


    public int TotalCoins { get => totalCoins; set => totalCoins = value; }

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        manager = GameObject.Find("Firebase Manager").GetComponent<FirebaseManager>();
            
        coins = 0;
        coinText.text = coins.ToString();

        score = 0;
        scoreText.text = "0";
       
        TempoDash = MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo];
        TempoIma = MenuManager.instance.DurationMagnet[MenuManager.instance.LevelMagnet];
        turboFuel = MenuManager.instance.DurationTurbo[MenuManager.instance.LevelTurbo];
        turboSlider.value = turboFuel;

    }


    // Update is called once per frame
    void Update()
    {
        
        if(!gameOver)
        {
            if (!isDash)
            {
                gasoline -= Time.deltaTime;


                if (turboFuel < TempoDash)
                {
                    turboFuel += Time.deltaTime*10;
                    turboSlider.value = turboFuel;
                }
            }
            else
            {
                turboFuel -= Time.deltaTime;

            }
           
            gasolineSlider.value = gasoline;
            turboSlider.value = turboFuel;
        }

        if (isDash)
        {
            timedash += Time.deltaTime;
        }

        if (isIma)
        {
            timeIma += Time.deltaTime;
        }



        backgroundTimer += Time.deltaTime;
        if (backgroundTimer > 60)
        {
            foregroundIndex++;
            skyIndex++;
            groundIndex++;
            transitionForeground = true;
            transitionGround = true;
            transitionSky = true;
            backgroundTimer = 0;
            orderMap++;
            if (orderMap == 4)
            {
                orderMap = 0;
            }
        }


        scrollSpeed -= (Time.deltaTime * 0.02f);

        if (isDash && !gameOver && timedash > TempoDash)
        {
            Time.timeScale = 1f;
            isDash = false;
            timedash = 0;
            scrollSpeed= tempSpeed;
        }

        if (isIma && !gameOver && timeIma > TempoIma)
        {
            isIma = false;
            timeIma = 0;
        }
    }

    IEnumerator CountTo(float targetValue, float currentValue)
    {

        coinEffect.SetActive(true);
        var rate = Mathf.Abs(targetValue/2f);

        coinGO.text = currentValue.ToString();
        targetValue += currentValue;
       
        while (currentValue != targetValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);
            coinGO.text = ((int)currentValue).ToString();
            coinText.text = ((int)(targetValue - currentValue)).ToString();
            FindObjectOfType<AudioManager>().Play("points");
            yield return null;
            
        }
        coinEffect.SetActive(false);
    }

    IEnumerator CountToHighscore(float targetValue)
    {
        var currentValue = 0f;
        var hs = MenuManager.instance.Highscore;
        var rate = Mathf.Abs(targetValue/ 2f);
        bool hstrue = true;
        highscoreGO.text = "0";
        while (currentValue != targetValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);
            highscoreGO.text = ((int)currentValue).ToString();
            scoreText.text = ((int)(targetValue - currentValue)).ToString();
            
            if (currentValue > hs && hstrue)
            {
                FindObjectOfType<AudioManager>().Play("positive");
                highScore.SetActive(true);
                hstrue = false;
            }
            yield return null;
        }

    }
    public void ContinueBtn()
    {
        alreadyContinue = true;
        Continue.SetActive(false);
        gameOver = false;
        DisableObstacles();
        FindObjectOfType<bird>().RepositionContinue();
    }
    private void DisableObstacles()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Coluna");

        foreach (GameObject go in gameObjectArray)
        {
            go.SetActive(false);
        }
    }
    public void BirdDied()
    {
        gameOver = true;
        if (!alreadyContinue)
        {
            Continue.SetActive(true);
        }
        else
        {
            GameOverScreen();
        }

    }

    public void GameOverScreen()
    {
        //fazer efeito https://www.youtube.com/watch?v=CfX002SPWmU&ab_channel=Unity3DSchool

        StartCoroutine(CountTo(coins, MenuManager.instance.TotalCoins));
        StartCoroutine(CountToHighscore(score));




        //coinGO.text = MenuManager.instance.TotalCoins.ToString();
        ////coinText.text = coins.ToString();
        //highscoreGO.text = score.ToString();
        gameOverText.SetActive(true);
        gameHUD.SetActive(false);
        gameOver = true;

        MenuManager.instance.TotalCoins += coins;  // current value
        coins = 0;   // target value

        if (score > MenuManager.instance.Highscore)
        {
            MenuManager.instance.SetHighscore(score.ToString());
        }
        score = 0;
        manager.SaveDataButton();
    }

    public void BirdScored()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        highscoretext.text = score.ToString();
       

    }

    public void CoinPickUp()
    {
        if (gameOver)
        {
            return;
        }
        coins++;
        //PlayerPrefs.SetInt("Coins", coins);
        //coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        coinText.text = coins.ToString();
        score += 1;
        highscoretext.text = score.ToString();



    }

    public void Dash()
    {
        if (gameOver)
        {
            return;
        }
        //Time.timeScale = 5f;
        
        timedash = 0;
        if (!isDash)
        {
            tempSpeed = scrollSpeed;
        }
        if (!isDash)
        {
            FindObjectOfType<AudioManager>().Play("Aura");
            scrollSpeed *= 5;
            isDash = true;
        }

        score += 10;
        highscoretext.text = score.ToString();
    }

    public void GasolinaPickUp()
    {
        gasoline += MenuManager.instance.QuantityGas[MenuManager.instance.LevelGas];
        if (gasoline > 100) { gasoline = 100; }
        score += 10;
        highscoretext.text = score.ToString();
    }

    public void ImaPickUp()
    {
        isIma = true;
        timeIma = 0;
        score += 10;
        highscoretext.text = score.ToString();
    }

}
