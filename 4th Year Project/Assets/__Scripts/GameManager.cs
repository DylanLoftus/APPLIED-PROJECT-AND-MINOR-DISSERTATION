using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float outsideTemp;
    public WeatherHistory weatherHistory = new WeatherHistory();

    [SerializeField]
    private Room[] rooms;

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

    public IEnumerator RunSimulation()
    {

        Debug.Log("Gamemanager received!");
        for (int i = 0; i < weatherHistory.Length; i++)
        {
            outsideTemp = weatherHistory.Data[i].Temperature;
            Debug.Log("New outside temperature is: " + outsideTemp);
            for (int j = 0; j < 5; j++)
            {
                // simulate 1 second (equalise temperatures)
                EqualizeTemperatures();
                yield return new WaitForSeconds(1.0f);
            }
        }
        Debug.Log("Outside loop");
    }

    public void EqualizeTemperatures()
    {
        foreach (Room room in rooms) {
            room.EqualizeTemperature();
        }
    }

    public void SetWeatherData(WeatherHistory weatherHistory)
    {
        this.weatherHistory = weatherHistory;
        StartCoroutine(RunSimulation());
    }
}
