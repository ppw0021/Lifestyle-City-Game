using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public Image icon;
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
        icon.sprite = plant.icon;
    }
}