using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignGoogle : MonoBehaviour
{
public void Googlesignbtn()
    {
        FindObjectOfType<FirebaseManager>().GoogleSignInClick();
    }

    public void Facebtn()
    {
        FindObjectOfType<FirebaseManager>().Facebook_Login();
    }
}
