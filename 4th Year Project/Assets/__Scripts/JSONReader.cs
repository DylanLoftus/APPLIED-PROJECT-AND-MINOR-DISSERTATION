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
        UnityWebRequest www = UnityWebRequest.Get(GetUserChosenDatasetLink());

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] results = www.downloadHandler.data;
            deserializeBytes(results);
        }
    }

    private string GetUserChosenDatasetLink()
    {
        try
        {
            WeatherListingReader reader = GameObject.Find("WeatherListingReader").GetComponent<WeatherListingReader>();
            return reader.chosenDatasetLink;
        }
        catch (NullReferenceException e)
        {
            // default value if the weather selection scene wasn't used
            return "https://ronanstuff.strangled.net:53269/Ronan-H/weather-api/1.0.0/historical/athenry/14";
        }
    }

    public void deserializeBytes(byte[] results)
    {
        
        string str = System.Text.Encoding.Default.GetString(results);
        WeatherHistory forecast = JsonUtility.FromJson<WeatherHistory>(str);
        gameManager.SetWeatherData(forecast);
    }
}
