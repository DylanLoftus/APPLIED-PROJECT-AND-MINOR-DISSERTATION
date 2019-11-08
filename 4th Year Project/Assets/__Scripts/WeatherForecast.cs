﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherForecast
{
    public string Description { get; set; }
    public int length { get; set; }
    public string Summary { get; set; }
    public IList<DataPoint> Data { get; set; }
}

public class DataPoint
{
    public string Timestamp { get; set; }
    public float Temperature { get; set; }
    public float Windspeed { get; set; }
}