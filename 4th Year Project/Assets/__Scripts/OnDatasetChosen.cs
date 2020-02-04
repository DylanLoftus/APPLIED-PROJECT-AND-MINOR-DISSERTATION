using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnDatasetChosen : MonoBehaviour
{
    public void OnButtonClicked()
    {
        int dataset = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        WeatherListingReader reader = GameObject.Find("WeatherListingReader").GetComponent<WeatherListingReader>();
        string datasetLink = reader.listings.listings[dataset].link;
        reader.chosenDatasetLink = datasetLink;
        SceneManager.LoadSceneAsync(1);
    }
}
