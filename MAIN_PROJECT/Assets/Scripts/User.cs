using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
[System.Serializable]
public class User
{
    public int user_id = -1;
    public string username = "UNLOADED";
    public string answer = "UNLOADED";
    public string sesh_token = "UNLOADED";
    public int level = -1;
    public int coins = 99999;
    public int xp = -1;

    public void printDetails()
    {
        Debug.Log("user_id: " + user_id);
        Debug.Log("username: " + username);
        Debug.Log("answer: " + answer);
        Debug.Log("sesh_token: " + sesh_token);
        Debug.Log("level: " + level);
        Debug.Log("coins: " + coins);
        Debug.Log("xp: " + xp);
    }

    /*
    public int ResultValidity()
    {
        if (user_id >= 0)
        {
            //User authenticated
            Debug.Log("User Authenticated");
            return 0;
        }
        if (user_id == -1)
        {
            //User does not exist
            Debug.Log("User does not exist");
            return -1;
        }
        if (user_id == -2)
        {
            //Password not correct
            Debug.Log("Password incorrect");
            return -2;
        }
        if (user_id == -3)
        {
            //Uninitialized
            Debug.Log("Uninitialised");
            return -3;
        }
        //You're cooked mate
        Debug.Log("This should not happen, ever");
        return -4;
    }*/

}
