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
    public Button proceedButton;
    
    void Start()
    {
        proceedButton.onClick.AddListener(onProceedButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onProceedButtonClick()
    {
        Debug.Log(newUsername + " " + answer + " " + newPassword + " " + verifyPassword);
    }
}
