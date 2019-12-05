using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float outsideTemp;
    public WeatherHistory weatherHistory = new WeatherHistory();

    [SerializeField]
    private Room[] rooms;

    [SerializeField]
    private Door[] doors;

    public IEnumerator RunSimulation()
    {

        Debug.Log("Gamemanager received!");
        for (int i = 0; i < weatherHistory.length; i++)
        {
            outsideTemp = weatherHistory.data[i].temperature;
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
        // add heat to rooms if the radiator is on
        foreach (Room room in rooms)
        {
            Radiator radiator = room.GetComponentInChildren<Radiator>();

            if (radiator.isOn)
            {
                room.roomTemperature += 1;
            }
        }

        // equalise temperatures between each room and the outside weather
        foreach (Room room in rooms) {
            room.EqualiseTempToOutside();
        }

        // equalise temperatures between rooms
        foreach (Door door in doors)
        {
            door.EqualiseTempBetweenRooms();
        }
    }

    public void SetWeatherData(WeatherHistory weatherHistory)
    {
        this.weatherHistory = weatherHistory;
        StartCoroutine(RunSimulation());
    }
}
