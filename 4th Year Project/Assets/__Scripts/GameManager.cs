using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float outsideTemp;
    public WeatherForecast f = new WeatherForecast();
    public bool windowOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void DoorOpen(Room[] connectingRooms)
    {
        foreach(Room r in connectingRooms){
            r.roomTemperature -= 5;
            Debug.Log(r.roomTemperature);
        }

    }

    public void WindowOpen(Room room)
    {
        room.roomTemperature -= 8;
        Debug.Log(room.roomTemperature);
    }

    public IEnumerator setWeatherForecast()
    {

        Debug.Log("Gamemanager received!");
        for (int i = 1; i < 180; i++)
        {
            Debug.Log("Got into loop");
            Debug.Log(f.Data[i].Temperature);
            outsideTemp = f.Data[i].Temperature;
            Debug.Log("Outside temperature is: " + outsideTemp);
            yield return new WaitForSeconds(5.0f);
        }
        Debug.Log("Outside loop");
    }

    public void setForecast(WeatherForecast forecast)
    {
        f = forecast;
        StartCoroutine(setWeatherForecast());
    }
}
