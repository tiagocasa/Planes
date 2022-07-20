using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarChange : MonoBehaviour
{
    [SerializeField] private int AvatarId;


    private void Start()
    {
        if(MenuManager.instance.AvatarId == AvatarId)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Avatar()
    {
        FindObjectOfType<AudioManager>().Play("botao");
        MenuManager.instance.AvatarId = AvatarId;
        StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateAvatarId(MenuManager.instance.AvatarId));
    }
}
