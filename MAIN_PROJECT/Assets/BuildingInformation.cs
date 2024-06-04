using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInformation : MonoBehaviour
{
    private int xpos;
    private int ypos;
    private int strucId;
    private int instId;

    // Getter methods for each variable
    public int GetXPOS()
    {
        return xpos;
    }

    public int GetYPOS()
    {
        return ypos;
    }

    public int GetSTRUCID()
    {
        return strucId;
    }

    public int GetINSTID()
    {
        return instId;
    }

    // Method to initialize the variables
    public void init(int xpos, int ypos, int strucId, int instId)
    {
        this.xpos = xpos;
        this.ypos = ypos;
        this.strucId = strucId;
        this.instId = instId;
    }


}