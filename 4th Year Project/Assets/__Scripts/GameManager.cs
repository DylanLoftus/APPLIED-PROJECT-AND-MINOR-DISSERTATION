using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float outsideTemp;
    public WeatherHistory weatherHistory = new WeatherHistory();
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

    public IEnumerator runSimulation()
    {

        Debug.Log("Gamemanager received!");
        for (int i = 0; i < weatherHistory.Length; i++)
        {
            outsideTemp = weatherHistory.Data[i].Temperature;
            Debug.Log("Outside temperature is: " + outsideTemp);
            for (int j = 0; j < 5; j++)
            {
                // simulate 1 second (equalise temperatures)

                yield return new WaitForSeconds(1.0f);
            }
        }
        Debug.Log("Outside loop");
    }

    public void setWeatherData(WeatherHistory weatherHistory)
    {
        this.weatherHistory = weatherHistory;
        StartCoroutine(runSimulation());
    }
}
