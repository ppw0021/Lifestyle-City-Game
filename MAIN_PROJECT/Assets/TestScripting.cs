using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripting : MonoBehaviour
{
    //public InterfaceAPI testAPI;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current User is: " + InterfaceAPI.currentUser.username);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
