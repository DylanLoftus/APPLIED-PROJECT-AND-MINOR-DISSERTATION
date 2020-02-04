using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeatherListingReader : MonoBehaviour
{
    [SerializeField]
    private GameObject listingButton;

    GameManager gameManager;
    public WeatherListings listings;
    public string chosenDatasetLink;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

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
            byte[] results = www.downloadHandler.data;
            deserializeBytes(results);
        }
    }

    public void deserializeBytes(byte[] results)
    {
        // adding an outer JSON object here since JsonUtility.FromJson doesn't accept json arrays
        string str = "{\"listings\":" + System.Text.Encoding.Default.GetString(results) + "}";
        listings = JsonUtility.FromJson<WeatherListings>(str);

        for (int i = 0; i < listings.listings.Length; i++)
        {
            WeatherListing listing = listings.listings[i];
            GameObject button = Instantiate(listingButton);
            button.name = i.ToString();
            button.transform.SetParent(GameObject.FindGameObjectWithTag("WeatherSelectContent").transform, false);
            button.transform.position = new Vector3(0, 0, 0);
            button.transform.localPosition = new Vector3(0, 0, 0);

            string desc = string.Format("{0} #{1}, starts at {2}, {3} realtime hours",
                listing.area, listing.dataset + 1, listing.start_time, listing.length);

            button.GetComponentInChildren<Text>().text = desc;
        }

        GameObject.FindGameObjectWithTag("WeatherSelectPrompt").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Choose a weather data set";
    }
}
