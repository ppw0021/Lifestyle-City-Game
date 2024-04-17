using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class InterfaceAPI : MonoBehaviour
{
    //public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://penushost.ddns.net/api"));       //Test example
    }


    IEnumerator GetRequest(string uri)      //This function tests the connection using a get request and converts the results to an obect
    {
        //Create GET request
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            
            yield return webRequest.SendWebRequest();                                                   //Send the get request
            switch (webRequest.result)                                                                  //When request arrives
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:                                                    //Information recieved successfully 
                    string jsonRaw = webRequest.downloadHandler.text;                                   //Get RAW JSON
                    string strippedString = StripSquareBrackets(jsonRaw);                               //Strip [] array brackets as it is sent as an array
                    DataPacket data = JsonUtility.FromJson<DataPacket>(strippedString);                 //Deserialize JSON string into a DataPacket object      
                    Debug.Log("ID: " + data.id);                                                        //Now you can access the properties of the DataPacket object
                    Debug.Log("First Name: " + data.first_name);
                    Debug.Log("Last Name: " + data.last_name);
                    Debug.Log("Role: " + data.role);
                    //text.text = data.first_name;
                    break;
            }
        }
    }

    IEnumerator SendJsonRequest(string uri, string jsonData)    //This function sends a POST request to a specified URI with a JSON payload
    {
        //Create POST request
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, "application/json"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);          //Convert JSON to byte array
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);                    //Attach JSON to request
            webRequest.SetRequestHeader("Content-Type", "application/json");                // Set the content type
            yield return webRequest.SendWebRequest();                                       //Send the actual request
        }
    }


    string StripSquareBrackets(string input)
    {
        if (input.Length < 2)
        {
            // If the string has less than 2 characters, return it as is.
            return input;
        }
        else
        {
            // Check if the first and last characters are square brackets, and remove them if they are.
            if (input[0] == '[' && input[input.Length - 1] == ']')
            {
                return input.Substring(1, input.Length - 2);
            }
            else
            {
                // If the string does not start and end with square brackets, return it as is.
                return input;
            }
        }
    }

    
}
