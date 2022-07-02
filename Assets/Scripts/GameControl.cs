using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using System.Media;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    private FirebaseManager manager;



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
    public Text highScore;

    private int score = 0;



    private int coins = 0;
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

        //manager = GameObject.Find("Firebase Manager").GetComponent<FirebaseManager>();

        coins = 0;
        coinText.text = coins.ToString();

        //highScore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();

        //coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        //coins = PlayerPrefs.GetInt("Coins", 0);
        //scoreText.text = "SCORE:" + score.ToString();

        // Botão do add 
        if (PlayerPrefs.GetInt("Continue", 0) == 1)
        {
            score = PlayerPrefs.GetInt("Score", 0);
            scoreText.text = "SCORE:" + PlayerPrefs.GetInt("Score", 0).ToString();
        }
        else
        {
            score = 0;
            scoreText.text = "SCORE:0";
        }

        PlayerPrefs.SetString("nome od maluco", "Egil o bebado");



        TempoDash = 5f;//PlayerPrefs.GetInt("TEMPO DO TURBO", 0) * 10f;
        TempoIma = 15f;//PlayerPrefs.GetInt("TEMPO DO IMÃ", 0) * 5f;

        if (PlayerPrefs.GetInt("Continue", 0) == 1)
        {
            PlayerPrefs.SetInt("Continue", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Continue", 1);
        }

        //coinGO.text = totalCoins.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        gasoline -= Time.deltaTime;
        if (gasoline <= 0)
        {
            gameOver = true;
        }
        else
        {
            gasolineSlider.value = gasoline;
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


        scrollSpeed -= (Time.deltaTime * 0.05f);

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



    public void BirdDied()
    {
        //fazer efeito https://www.youtube.com/watch?v=CfX002SPWmU&ab_channel=Unity3DSchool
        totalCoins += coins;
        coins = 0;
        //manager.SaveDataButton();


        coinGO.text = totalCoins.ToString();
        coinText.text = coins.ToString();
        highscoreGO.text = score.ToString();
        gameOverText.SetActive(true);
        gameHUD.SetActive(false);
        gameOver = true;
        PlayerPrefs.SetInt("Score", score);


        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            // Efeito de parabens
            highScore.text = "HIGHSCORE:" + score.ToString();
        }
        

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
            scrollSpeed *= 5;
            isDash = true;
        }


    }

    public void GasolinaPickUp()
    {
        gasoline = 100;
        if (gasoline > 100) { gasoline = 100; }
    }

    public void ImaPickUp()
    {
        isIma = true;
        timeIma = 0;
    }

}
