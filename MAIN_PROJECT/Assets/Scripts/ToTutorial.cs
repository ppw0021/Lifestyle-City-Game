using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour
{
    public void OnTutorialButtonClick()
    {
        SceneManager.LoadScene("TutorialSection");
    }
}