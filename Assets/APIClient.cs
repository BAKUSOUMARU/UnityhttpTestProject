using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIClient : MonoBehaviour
{
    public string apiURL = "http://bakusoumaru.site:3000/api/get_text"; // ご自身のAPIのURLに置き換えてください

    void Start()
    {
        StartCoroutine(GetTextFromAPI());
    }

    IEnumerator GetTextFromAPI()
    {
        
        Debug.Log("接続中");
        UnityWebRequest www = UnityWebRequest.Get(apiURL);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string textResponse = www.downloadHandler.text;
            Debug.Log("API Response: " + textResponse);
            // ここでtextResponseを処理または表示することができます
        }
    }
}
