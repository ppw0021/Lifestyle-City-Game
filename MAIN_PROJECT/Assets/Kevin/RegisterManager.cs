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
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string repeatPassword = repeatPasswordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
        {
            errorMessage.text = "Please fill in all fields.";
        }
        else if (!IsValidEmail(email))
        {
            errorMessage.text = "Invalid email address.";
        }
        else if (username.Length < 1)
        {
            errorMessage.text = "Username cannot be empty.";
        }
        else if (password.Length < 8)
        {
            errorMessage.text = "Password must be 8 characters or longer.";
        }
        else if (!ContainsCapitalLetter(password))
        {
            errorMessage.text = "Password must contain at least one capital letter.";
        }
        else if (!ContainsSpecialSymbol(password))
        {
            errorMessage.text = "Password must contain at least one special symbol.";
        }
        else if (password != repeatPassword)
        {
            errorMessage.text = "Passwords do not match.";
        }
        else
        {
            // Save the user details to PlayerPrefs to pass them to the SecurityQuestionScene
            PlayerPrefs.SetString("email", email);
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetString("password", password);

            // Proceed to the security question scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("SecurityManager");
        }
    }

    // Method to validate the password: check if it contains at least one capital letter
    bool ContainsCapitalLetter(string password)
    {
        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                return true;
            }
        }
        return false;
    }

    // Method to validate the password: check if it contains at least one special symbol
    bool ContainsSpecialSymbol(string password)
    {
        foreach (char c in password)
        {
            if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
            {
                return true;
            }
        }
        return false;
    }

    // Method to validate the email
    bool IsValidEmail(string email)
    {
        string[] validDomains = new string[]
        {
            "outlook.com", "hotmail.com", "live.com", "msn.com","gmail.com",
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