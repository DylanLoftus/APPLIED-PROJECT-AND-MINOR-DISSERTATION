using System.Collections.Generic;

[System.Serializable]
public class WeatherHistory
{
    public string description;
    public int length;
    public List<DataPoint> data;
}

[System.Serializable]
public class DataPoint
{
    public string timestamp;
    public float temperature;
    public float windspeed;
}

[System.Serializable]
public class WeatherListing
{
    public string area;
    public int dataset;
    public int length;
    public string link;
    public string start_time;
}

[System.Serializable]
public class WeatherListings
{
    public WeatherListing[] listings;
}