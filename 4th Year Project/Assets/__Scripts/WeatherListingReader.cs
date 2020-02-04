using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherListingReader : MonoBehaviour
{

    GameManager gameManager;

    void Start()
    {
        StartCoroutine(GetListings());
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    IEnumerator GetListings()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://ronanstuff.strangled.net:53269/Ronan-H/weather-api/1.0.0/historical");
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
        // adding an outer JSON object here since JsonUtility.FromJson doesn't accept json arrays
        string str = "{\"listings\":" + System.Text.Encoding.Default.GetString(results) + "}";
        WeatherListings listings = JsonUtility.FromJson<WeatherListings>(str);
        // gameManager.SetWeatherData(forecast);
        foreach (WeatherListing listing in listings.listings) {
            Debug.Log("Area: " + listing.area);
            Debug.Log("Dataset: " + listing.dataset);
            Debug.Log("Length: " + listing.length);
            Debug.Log("Link: " + listing.link);
            Debug.Log("Start time: " + listing.start_time + "\n");
        }
    }
}
