using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using System.Media;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject gameHUD;
    public bool gameOver = false;
    public bool gamePaused = false;
    public float gasoline = 2f;

    public float scrollSpeed = -1.5f;
    public float incrementSpeed = 2f;


    public Text scoreText;
    public Text highScore;
    private int score = 0;
    private int coins;
    public Text coinText;
    public Text coinGO;

    public bool isDash = false;
    private float timedash=0;

    public bool isIma = false;
    private float timeIma = 0;

    private float TempoIma;
    private float TempoDash;



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

        highScore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();

        coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        coins = PlayerPrefs.GetInt("Coins", 0);
        scoreText.text = "SCORE:" + score.ToString();

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

        TempoDash = PlayerPrefs.GetInt("TEMPO DO TURBO", 0) * 10f;
        TempoIma = PlayerPrefs.GetInt("TEMPO DO IMÃ", 0) * 5f;

        if (PlayerPrefs.GetInt("Continue", 0) == 1)
        {
            PlayerPrefs.SetInt("Continue", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Continue", 1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        gasoline -= Time.deltaTime; 

        timedash += Time.deltaTime;

        timeIma += Time.deltaTime;

        if (isDash && !gameOver && timedash > TempoDash)
        {
            Time.timeScale = 1f;
            isDash = false;
            timedash = 0;
        }

        if (isIma && !gameOver && timeIma > TempoIma)
        {
            isIma = false;
            timeIma = 0;
        }
    }



    public void BirdDied()
    {
        coinGO.text = PlayerPrefs.GetInt("Coins", 0).ToString();
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
        scoreText.text = "SCORE:" + score.ToString();
       

    }

    public void CoinPickUp()
    {
        if (gameOver)
        {
            return;
        }
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        
    }

    public void Dash()
    {
        if (gameOver)
        {
            return;
        }
        Time.timeScale = 5f;
        isDash = true;
        timedash = 0;


    }

    public void GasolinaPickUp()
    {
        gasoline = 100;
    }

    public void ImaPickUp()
    {
        isIma = true;
        timeIma = 0;
    }

}
