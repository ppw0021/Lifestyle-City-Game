using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterManager : MonoBehaviour
{
     // UI Components
    public TMP_InputField emailInputField;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField repeatPasswordInputField;
    public TMP_Text errorMessage;
    public Button backButton;
    public Button proceedButton;

    // Start is called before the first frame update
    void Start()
    {
        errorMessage.text = "";
        backButton.onClick.AddListener(OnBackButtonClick);
        proceedButton.onClick.AddListener(OnProceedButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to handle back button click
    void OnBackButtonClick()
    {
        // Load the login scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login (testing)");
    }

    // Method to handle proceed button click
    void OnProceedButtonClick()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        string repeatPassword = repeatPasswordInputField.text;

        if (!IsValidEmail(email))
        {
            errorMessage.text = "Invalid email address.";
        }
        else if (!IsStrongPassword(password))
        {
            errorMessage.text = "Password must be at least 8 characters long, contain at least one uppercase letter, and one symbol.";
        }
        else if (password != repeatPassword)
        {
            errorMessage.text = "Passwords do not match.";
        }
        else
        {
            // Proceed to the security question scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Secuirty Questions");
        }
    }

    // Method to validate the password
    bool IsStrongPassword(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]"))
        {
            return false;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]"))
        {
            return false;
        }
        return true;
    }

    // Method to validate the email
    bool IsValidEmail(string email)
    {
        string[] validDomains = new string[]
        {
            "outlook.com", "hotmail.com", "live.com", "msn.com",
            "yahoo.com", "ymail.com", "rocketmail.com",
            "protonmail.com", "protonmail.ch",
            "icloud.com", "me.com", "mac.com",
            "zoho.com",
            "aol.com",
            "gmx.com", "gmx.net",
            "mail.com", "email.com", "usa.com", "consultant.com",
            "yandex.com", "yandex.ru",
            "tutanota.com", "tutanota.de"
        };

        string domain = email.Substring(email.IndexOf('@') + 1);
        foreach (string validDomain in validDomains)
        {
            if (domain == validDomain)
            {
                return true;
            }
        }
        return false;
    }
}
