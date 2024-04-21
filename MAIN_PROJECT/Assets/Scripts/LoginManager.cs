using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LoginManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public TMP_InputField usernameEmail;
    public TMP_InputField passwordInput;
    //public InterfaceAPI interfaceAPIObject;

    public void LoginButtonPressed() {
        //Debug.Log(usernameEmail.text);
        StartCoroutine(InterfaceAPI.LoginPost("https://penushost.ddns.net/login", "{\"username\": \"" + usernameEmail.text + "\", \"password\": \"" + passwordInput.text + "\"}"));
    }
}
