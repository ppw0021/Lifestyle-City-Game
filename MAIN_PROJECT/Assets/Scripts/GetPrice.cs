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
    // Start is called before the first frame update
    void Start()
    {
        int cost = database.objectsData[ID].Cost;
        text.text = ("$" + cost);
    }
}
