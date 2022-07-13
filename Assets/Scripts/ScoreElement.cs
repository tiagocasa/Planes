using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text highscore;
    public Image avatar;

    public GameObject gold;
    public GameObject silver;
    public GameObject bronze;


   public void NewScoreElement(int position, string _username, int _highscore, int _avatarid)
    {
        if(position == 1)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            //VER LOGICA AVATAR
            gold.SetActive(true);
            silver.SetActive(false);
            bronze.SetActive(false);
        }
        else if(position == 2)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            //VER LOGICA AVATAR
            gold.SetActive(false);
            silver.SetActive(true);
            bronze.SetActive(false);
        }
        else if (position == 3)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            //VER LOGICA AVATAR
            gold.SetActive(false);
            silver.SetActive(false);
            bronze.SetActive(true);
        }
        else
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            //VER LOGICA AVATAR
            gold.SetActive(false);
            silver.SetActive(false);
            bronze.SetActive(false);
        }
      
    }
}
