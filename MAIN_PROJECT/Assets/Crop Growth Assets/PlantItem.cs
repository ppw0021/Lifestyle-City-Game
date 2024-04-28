using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = plant.icon;
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
    }
}