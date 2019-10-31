using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{
    string path;
    string jsonData;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.streamingAssetsPath + "/temperature.json";
        jsonData = File.ReadAllText(path);
        SpudTest spud = JsonUtility.FromJson<SpudTest>(jsonData);
        Debug.Log("Reading in from JSON file: " + spud.id);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class SpudTest
    {
        public string id;
        public int[] temperature;
    }
}
