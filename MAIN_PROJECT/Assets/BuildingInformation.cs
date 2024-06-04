using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInformation : MonoBehaviour
{
    int XPOS;
    int YPOS;
    int STRUCID;
    int INSTID;

    public void init(int xpos, int ypos, int strucid, int instid)
    {
        XPOS = xpos;
        YPOS = ypos;
        STRUCID = strucid;
        INSTID = instid;
    }
}
