using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] Skins;
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject changeAvatarScreen;

    private void Start()
    {
        SkinSelect();
    }
    public void SkinSelect()
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            Skins[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OpenScreen()
    {
        if (FindObjectOfType<FirebaseManager>().IsAnonimo())
        {
            FindObjectOfType<AudioManager>().Play("botao");
            loginScreen.SetActive(true);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("botao");
            changeAvatarScreen.SetActive(true);
        }

    }
}
