using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    private FirebaseManager fm;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FirebaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
      //  if (SceneManager.GetActiveScene().buildIndex == 0) {ScreenUpdate(); }
    }
    public void ScreenUpdate()
    {
        highscoreText.text = MenuManager.instance.Highscore.ToString();
        coinText.text = MenuManager.instance.TotalCoins.ToString();
        cashText.text = MenuManager.instance.Cash.ToString();
        nameText.text = fm.UserName();
    }
}
