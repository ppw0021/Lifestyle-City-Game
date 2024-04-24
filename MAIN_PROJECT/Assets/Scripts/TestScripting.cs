using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripting : MonoBehaviour
{
    //public InterfaceAPI testAPI;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Current User is: " + InterfaceAPI.currentUser.username);
        //StartCoroutine(InterfaceAPI.GetBasePost("https://penushost.ddns.net/getbase", "{\"sesh_id\": \"" + InterfaceAPI.currentUser.sesh_token + "\", \"user_id\": " + InterfaceAPI.currentUser.user_id + "}"));
        //StartCoroutine(InterfaceAPI.currentUser.setCoins(1000));
        StartCoroutine(InterfaceAPI.setCoins(1000));
        for (int i = 0; i < InterfaceAPI.buildingList.Count; i++)
        {
            InterfaceAPI.buildingList[i].printDetails();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
