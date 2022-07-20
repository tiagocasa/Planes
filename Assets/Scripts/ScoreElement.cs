using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text highscore;
    public GameObject avatar;

    public GameObject gold;
    public GameObject silver;
    public GameObject bronze;
    public GameObject skinsprite;

    private SkinManager sm;


    public void NewScoreElement(int position, string _username, int _highscore, int _avatarid, string _skinid)
    {
        sm = GameObject.Find("New Menu").transform.GetChild(5).GetChild(2).transform.GetChild(2).GetComponent<SkinManager>();


        if (position == 1)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            if(_username != "Guest")
            {
                avatar.transform.GetChild(2).gameObject.SetActive(true);
                avatar.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<AvatarList>().spriteList[_avatarid];

            }
            gold.SetActive(true);
            silver.SetActive(false);
            bronze.SetActive(false);
            Sprite spriteSelected = sm.GetSkinRank(_skinid);
            skinsprite.GetComponent<Image>().sprite = spriteSelected;
        }
        else if(position == 2)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            if (_username != "Guest")
            {
                avatar.transform.GetChild(2).gameObject.SetActive(true);
                avatar.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<AvatarList>().spriteList[_avatarid];
            }
            gold.SetActive(false);
            silver.SetActive(true);
            bronze.SetActive(false);
            Sprite spriteSelected = sm.GetSkinRank(_skinid);
            skinsprite.GetComponent<Image>().sprite = spriteSelected;
        }
        else if (position == 3)
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            if (_username != "Guest")
            {
                avatar.transform.GetChild(2).gameObject.SetActive(true);
                avatar.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<AvatarList>().spriteList[_avatarid];
            }
            gold.SetActive(false);
            silver.SetActive(false);
            bronze.SetActive(true);
            Sprite spriteSelected = sm.GetSkinRank(_skinid);
            skinsprite.GetComponent<Image>().sprite = spriteSelected;
        }
        else
        {
            username.text = _username;
            highscore.text = "Highscore: " + _highscore.ToString();
            if (_username != "Guest")
            {
                avatar.transform.GetChild(2).gameObject.SetActive(true);
                avatar.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<AvatarList>().spriteList[_avatarid];
            }
            gold.SetActive(false);
            silver.SetActive(false);
            bronze.SetActive(false);
            Sprite spriteSelected = sm.GetSkinRank(_skinid);
            skinsprite.GetComponent<Image>().sprite = spriteSelected;
        }
      
    }
}
