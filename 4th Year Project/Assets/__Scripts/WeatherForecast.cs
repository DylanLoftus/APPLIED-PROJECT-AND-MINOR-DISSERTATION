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
