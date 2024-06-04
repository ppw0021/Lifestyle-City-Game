using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour
{
    public TMP_InputField newUsername;
    public TMP_InputField answer;
    public TMP_InputField newPassword;
    public TMP_InputField verifyPassword;
    
    void Start()
    {
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    public void onProceedButtonClick()
    {
        Debug.Log(newUsername.text + " " + answer.text + " " + newPassword.text + " " + verifyPassword.text);
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        yield return StartCoroutine(InterfaceAPI.AddUser(newUsername.text, newPassword.text, answer.text));
        yield return StartCoroutine(InterfaceAPI.LoginPost(newUsername.text, newPassword.text));
    }
}
