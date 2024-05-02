using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    int plantStage = 0;
    float timer;
    PlantObject selectedPlant;
    FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = transform.parent.GetComponent<FarmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.growthTime;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        
        if (isPlanted)
        {
            if(plantStage == selectedPlant.plantStages.Length - 1)
            {
                Harvest();
            }
        }
        else if(fm.isPlanting)
        {
            Plant(fm.selectPlant.plant);
        }
    }

    void Harvest()
    {
        Debug.Log("Harvested");
        isPlanted = false;
        InterfaceAPI.addXp(25);
        int coins = InterfaceAPI.getCoins();
        coins += selectedPlant.reward;
        InterfaceAPI.Initialize(this);
        InterfaceAPI.setCoins(coins);
        plant.gameObject.SetActive(false);
    }

    void Plant(PlantObject newPlant)
    {
        
        selectedPlant = newPlant;
        if (selectedPlant.cost > InterfaceAPI.getCoins())
        {
            Debug.Log("No Money");
            return;
        }
        Debug.Log("Planted");
        isPlanted = true;
        int coins = InterfaceAPI.getCoins();
        coins -= selectedPlant.cost;
        InterfaceAPI.Initialize(this);
        InterfaceAPI.setCoins(coins);
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.growthTime;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
    }
}