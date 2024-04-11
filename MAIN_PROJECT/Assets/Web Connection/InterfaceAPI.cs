using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class InterfaceAPI : MonoBehaviour
{
    //public TextMeshProUGUI text;
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(GetRequest("https://penushost.ddns.net/api"));
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class UserList
    {
        public List<string> users { get; set; }
    }

    


    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    
                    string jsonRaw = webRequest.downloadHandler.text;
                    
                    string strippedString = StripSquareBrackets(jsonRaw);
                    // Deserialize JSON string into a DataPacket object
                    DataPacket data = JsonUtility.FromJson<DataPacket>(strippedString);
                    // Now you can access the properties of the DataPacket object
                    Debug.Log("ID: " + data.id);
                    Debug.Log("First Name: " + data.first_name);
                    Debug.Log("Last Name: " + data.last_name);
                    Debug.Log("Role: " + data.role);

                    text.text = data.first_name;
                    
                    break;
            }
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
