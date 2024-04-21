using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Response
{
    public string response = null;

    public void printResponse()
    {
        Debug.LogWarning(response);
    }
}
