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

    public BuildingInstance(string instance_id, string structure_id, string building_name, string x_pos, string y_pos, string owner_username)
    {
        this.instance_id = instance_id;
        this.structure_id = structure_id;
        this.building_name = building_name;
        this.x_pos = x_pos;
        this.y_pos = y_pos;
    }
    public void printDetails()
    {
        Debug.Log("Owner: " + owner_username + ", Instance ID: " + instance_id + ", Structure ID: " + structure_id + ", Building Name: " + building_name + ", X Pos: " + x_pos + ", Y Pos: " + y_pos + ".");
    }

}
