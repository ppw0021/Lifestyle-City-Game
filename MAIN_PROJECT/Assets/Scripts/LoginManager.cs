using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class LoginManager : MonoBehaviour
{


    [Header("UI")]
    public Text messageText;
    public TMP_InputField usernameEmail;
    public TMP_InputField passwordInput;
    //public InterfaceAPI interfaceAPIObject;
    

    public void LoginButtonPressed() {
        //Debug.Log(usernameEmail.text);
        StartCoroutine(WaitForLoginAndUserList());
    }

    private IEnumerator WaitForLoginAndUserList() {
        yield return StartCoroutine(InterfaceAPI.GetUseridList());
        yield return StartCoroutine(InterfaceAPI.LoginPost(usernameEmail.text, passwordInput.text));
    }
}
