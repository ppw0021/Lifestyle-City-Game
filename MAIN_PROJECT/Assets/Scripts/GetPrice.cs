using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetPrice : MonoBehaviour
{
    [SerializeField]
    private ObjectDatabaseSO database; 
    [SerializeField]
    private int ID;
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        int cost = database.objectsData[ID].Cost;
        text.text = ("$" + cost);
    }

    void Update()
    {
        int cost = database.objectsData[ID].Cost;
        if (cost > InterfaceAPI.getCoins())
        {
            text.color = Color.red;
            button.interactable = false;
        }
        else
        {
            text.color = Color.white;
            button.interactable = true;
        }
    }
}
