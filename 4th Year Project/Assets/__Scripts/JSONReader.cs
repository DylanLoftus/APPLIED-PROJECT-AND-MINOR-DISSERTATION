using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
        //UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8080/Ronan-H/weather-api/1.0.0/historical/athenry/14");
        //UnityWebRequest www = UnityWebRequest.Get("https://ronanstuff.strangled.net:53269/Ronan-H/weather-api/1.0.0/historical/athenry/14");

        UnityWebRequest www = GetUserChosenDataset();

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

    private UnityWebRequest GetUserChosenDataset()
    {
        string link = GameObject.Find("WeatherListingReader").GetComponent<WeatherListingReader>().chosenDatasetLink;
        Debug.Log("Link: " + link);
        return UnityWebRequest.Get(link);
    }

    public void deserializeBytes(byte[] results)
    {
        
        string str = System.Text.Encoding.Default.GetString(results);
        WeatherHistory forecast = JsonUtility.FromJson<WeatherHistory>(str);
        Debug.Log("Forcast data length: " + forecast.length);
        gameManager.SetWeatherData(forecast);
        Debug.Log("Sending to game manager!");
    }
}
