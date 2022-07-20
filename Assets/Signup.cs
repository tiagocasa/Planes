using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Signup : MonoBehaviour
{
    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    public void SignupBtn()
    {
        FindObjectOfType<FirebaseManager>().RegisterButton(emailRegisterField.text, passwordRegisterField.text, passwordRegisterVerifyField.text, usernameRegisterField.text);
    }

    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
        this.gameObject.SetActive(false);
    }

    public void Warning(string _msg)
    {
        warningRegisterText.text = _msg;
    }
}
