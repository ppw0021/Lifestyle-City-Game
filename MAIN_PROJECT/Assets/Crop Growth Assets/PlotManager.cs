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
        plant.gameObject.SetActive(false);
    }

    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        Debug.Log("Planted");
        isPlanted = true;
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