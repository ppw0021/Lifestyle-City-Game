using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class SecurityQuestionManager : MonoBehaviour
{
    public TMP_Dropdown securityQuestionDropdown;
    private void Start()
    {
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        
        List<string> options = new List<string> { "Select a question", "What is your childhood nickname?", "What is your first pet's name?", "What is your childhood dream job?" };
        securityQuestionDropdown.AddOptions(options);
    }
}