using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a new plant object
[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantObject : ScriptableObject
{
    public string plantName;        // Name of the plant
    public Sprite[] plantStages;    // Array of sprites representing different growth stages of the plant
    public float growthTime;        // Time taken for the plant to grow to the next stage
    public Sprite icon;             // Icon representing the plant
    public int cost;                // Cost of planting the plant
    public int reward;              // Reward gained from harvesting the plant
}