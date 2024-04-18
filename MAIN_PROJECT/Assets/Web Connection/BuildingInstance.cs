using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class BuildingInstance
{
    public int instance_id = -1;
    public int structure_id = -1;
    public string building_name = "";
    public int x_pos = -1;
    public int y_pos = -1;
    public string owner_username;
}
