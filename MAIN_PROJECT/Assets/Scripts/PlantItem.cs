using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI costTxt;
    public Image icon;
    public Image btnImage;
    public TextMeshProUGUI btnTxt;
    FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
        fm.SelectPlant(this);
    }

    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        costTxt.text = "$" + plant.cost;
        icon.sprite = plant.icon;
    }
}