using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingEventManager : MonoBehaviour
{
    public static BuildingEventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<BuildingInformation> OnBuildingClicked;

    public void BuildingClicked(BuildingInformation buildingInfo)
    {
        OnBuildingClicked?.Invoke(buildingInfo);
    }
}