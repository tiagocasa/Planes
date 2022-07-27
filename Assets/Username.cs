using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Username : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_Text error;
    public GameObject Scree;
    public void SetUsername()
    {
        if(username.text == "")
        {
            error.text = "User Cannont be Blank";
        }else if(username.text == "Guest" || username.text == "guest")
        {
            error.text = "User Cannont be Guest";
        }
        else
        {
            MenuManager.instance.Username = username.text;
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUserName(username.text));
            FindObjectOfType<NewMenu>().ScreenUpdate();
            Scree.SetActive(false);
        }
    }
}
