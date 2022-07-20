using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void LoginBtn()
    {
        FindObjectOfType<FirebaseManager>().LoginButton(emailLoginField.text, passwordLoginField.text);
    }

    public void Warning(string _msg)
    {
        warningLoginText.text = _msg;
    }

    public void Confirmation(string _msg)
    {
        confirmLoginText.text = _msg;
        this.gameObject.SetActive(false);
    }

    public void ForgotPass()
    {
        warningLoginText.text = "";
        if (emailLoginField.text == "")
        {
            warningLoginText.text = "Enter your email";
        }
        FindObjectOfType<FirebaseManager>().SendPasswordResetEmail(emailLoginField.text);
    }
}
