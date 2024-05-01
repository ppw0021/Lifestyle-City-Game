using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        InterfaceAPI.Initialize(this);
        InterfaceAPI.setXp(482);
        int x = InterfaceAPI.getXp();
        int level = InterfaceAPI.getLevel();
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
            selectPlant.btnImage.color = buyColor;
            selectPlant.btnTxt.text = "Buy";
            selectPlant = null;
            isPlanting = false;
        }
        else
        {
            if (selectPlant != null)
            {
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
            }
            selectPlant = newPlant;
            selectPlant.btnImage.color = cancelColor;
            selectPlant.btnTxt.text = "Cancel";
            Debug.Log("Selected " + selectPlant.plant.plantName);
            isPlanting = true;
        }
    }
}