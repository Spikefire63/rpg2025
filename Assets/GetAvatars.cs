using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class GetAvatars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetResults");
    }

    IEnumerator GetResults()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://192.168.56.102/php.list"))
        {
            //PlayerSettings.InsecureHttpOption.AlwaysAllowed;
           
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log("\nRecieved" + webRequest.downloadHandler.text);
                    break;

            }
        }
    }
}
