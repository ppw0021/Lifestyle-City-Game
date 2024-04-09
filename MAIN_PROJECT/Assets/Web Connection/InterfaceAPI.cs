using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

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
                    text.text = webRequest.downloadHandler.text;
                    break;
            }
        }
    }
}
