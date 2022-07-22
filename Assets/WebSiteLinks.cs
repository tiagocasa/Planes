using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebSiteLinks : MonoBehaviour
{
    public GameObject sigout;

    private void Start()
    {
        if (FindObjectOfType<FirebaseManager>().IsAnonimo())
        {
            sigout.GetComponent<Button>().interactable = false;
        }
        else
        {
            sigout.GetComponent<Button>().interactable = true;
        }
    }
    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/bighousemob");
    }

    public void OpenGooglePlay()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6645021760621907107");
    }

    public void OpenYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCZb1JDKpI7uaABYwRgwIt2A");
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/pp7VYhAmmK");
    }
    public void Signoff()
    {
        FindObjectOfType<FirebaseManager>().SignOutButton();
        Application.Quit();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
