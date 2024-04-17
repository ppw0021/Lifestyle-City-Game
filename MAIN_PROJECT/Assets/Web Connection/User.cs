using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class User
{
    public int user_id;
    public string username;
    public string sesh_token;
    public int level;
    public int coins;

    public void PrintDetails()
    {
        Debug.Log("user_id: " + user_id);
        Debug.Log("username: " + username);
        Debug.Log("sesh_token: " + sesh_token);
        Debug.Log("level: " + level);
        Debug.Log("coins: " + coins);
    }
}
