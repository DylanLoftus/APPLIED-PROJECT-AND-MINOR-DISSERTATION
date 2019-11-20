using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class JSONReader : MonoBehaviour
{

    GameManager gameManager;

    void Start()
    {
        StartCoroutine(GetText());
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    IEnumerator GetText()
    {
        // switch if hosting flask server locally
        //UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8080/Ronan-H/weather-api/1.0.0/historical/athenry/0");
        UnityWebRequest www = UnityWebRequest.Get("http://ronanstuff.strangled.net:53269/Ronan-H/weather-api/1.0.0/historical/athenry/0");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            deserializeBytes(results);
        }
    }

    public void deserializeBytes(byte[] results)
    {
        
        string str = System.Text.Encoding.Default.GetString(results);
        WeatherForecast forecast = JsonConvert.DeserializeObject<WeatherForecast>(str);
        gameManager.setForecast(forecast);
        Debug.Log("Sending to game manager!");
        
    }
}
