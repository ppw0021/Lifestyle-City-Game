using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScripting : MonoBehaviour
{
    //public InterfaceAPI testAPI;
    // Start is called before the first frame update
    void Start()
    {
        InterfaceAPI.Initialize(this);
        //Debug.Log("Current User is: " + InterfaceAPI.currentUser.username);
        //StartCoroutine(InterfaceAPI.GetBasePost("https://penushost.ddns.net/getbase", "{\"sesh_id\": \"" + InterfaceAPI.currentUser.sesh_token + "\", \"user_id\": " + InterfaceAPI.currentUser.user_id + "}"));
        //StartCoroutine(InterfaceAPI.currentUser.setCoins(1000));
        //InterfaceAPI.setCoins(123123);
        //InterfaceAPI.setLevel(31798);
        //InterfaceAPI.setUsername("d5star");
        //InterfaceAPI.addBuilding(0, 999, 999);
        //InterfaceAPI.printUser();
        //StartCoroutine(InterfaceAPI.setLevel(20));
        for (int i = 0; i < InterfaceAPI.buildingList.Count; i++)
        {
            InterfaceAPI.buildingList[i].printDetails();
        }

    }

    public void onXPAdd()
    {
        //InterfaceAPI.addXp(28);
        foreach (BuildingInstance buildInst in InterfaceAPI.buildingList)
        {
            buildInst.printDetails();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
