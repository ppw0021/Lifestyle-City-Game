using System.Collections;
using UnityEngine;
using TMPro;
using System;


public class CreateUser : MonoBehaviour
{
    public TMP_InputField newUsername;
    public TMP_InputField answer;
    public TMP_InputField newPassword;
    public TMP_InputField verifyPassword;
    public TextMeshProUGUI error;
    public void onProceedButtonClick()
    {
        Debug.Log(newUsername.text + " " + answer.text + " " + newPassword.text + " " + verifyPassword.text);
        if (newUsername.text == "")
        {
            //Fucked up
            error.text = "Empty username";
        }
        else if (newPassword.text == "")
        {
            error.text = "Empty password";
        }
        else if (newPassword.text != verifyPassword.text)
        {
            error.text = "Passwords do not match";
        }
        else
        {
            error.text = ""; 
            StartCoroutine(Login());
        }
        
    }

    private IEnumerator Login()
    {
        yield return StartCoroutine(InterfaceAPI.AddUser(newUsername.text, newPassword.text, answer.text));
        yield return StartCoroutine(InterfaceAPI.LoginPost(newUsername.text, newPassword.text));
        error.text = "records do not match"; 
    }
}
