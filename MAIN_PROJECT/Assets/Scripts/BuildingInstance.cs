using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class BuildingInstance
{
    public string instance_id = "";
    public string structure_id = "";
    public string building_name = "";
    public string x_pos = "";
    public string y_pos = "";
    public string owner_username;

    public void printDetails()
    {
        Debug.Log("Owner: " + owner_username + ", Instance ID: " + instance_id + ", Structure ID: " + structure_id + ", Building Name: " + building_name + ", X Pos: " + x_pos + ", Y Pos: " + y_pos + ".");
    }

}
