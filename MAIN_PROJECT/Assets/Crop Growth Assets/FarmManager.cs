using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    // Start is called before the first frame update
    void Start()
    {
        InterfaceAPI.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectPlant(PlantItem newPlant)
    {
        if (selectPlant == newPlant)
        {
            Debug.Log("Deselected " + selectPlant.plant.plantName);
            selectPlant = null;
            isPlanting = false;
        }
        else
        {
            selectPlant = newPlant;
            Debug.Log("Selected " + selectPlant.plant.plantName);
            isPlanting = true;
        }
    }
}