using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float outsideTemp;
    public WeatherHistory weatherHistory = new WeatherHistory();

    [SerializeField]
    private Room[] presetRooms;

    [SerializeField]
    private Door[] presetDoors;
    
    private IList<Room> rooms;
    private IList<Door> doors;

    private void Start()
    {
        InitialiseRooms();
    }

    void InitialiseRooms()
    {
        rooms = new List<Room>();
        doors = new List<Door>();

        foreach (Room room in presetRooms)
        {
            rooms.Add(room);
        }

        foreach (Door door in presetDoors)
        {
            doors.Add(door);
        }
    }

    public IEnumerator RunSimulation()
    {
        for (int i = 0; i < weatherHistory.length; i++)
        {
            DataPoint dataPoint = weatherHistory.data[i];
            outsideTemp = dataPoint.temperature;
            UpdateWeatherUI(dataPoint.timestamp, i);
            for (int j = 0; j < 5; j++)
            {
                // simulate 1 second (equalise temperatures)
                EqualizeTemperatures();
                yield return new WaitForSeconds(1.0f);
            }
        }
        Debug.Log("End of weather data set");
    }

    public void UpdateWeatherUI(string timestamp, int dataPointIndex)
    {
        TMPro.TextMeshProUGUI timestampComp = GameObject.FindGameObjectWithTag("WeatherTimestamp").GetComponent<TMPro.TextMeshProUGUI>();
        timestampComp.text = timestamp;

        TMPro.TextMeshProUGUI outsideTempComp = GameObject.FindGameObjectWithTag("OutsideTempText").GetComponent<TMPro.TextMeshProUGUI>();
        outsideTempComp.text = string.Format("It is {0}° outside", System.Math.Round(outsideTemp, 2));

        UnityEngine.UI.Slider slider = GameObject.FindGameObjectWithTag("ProgressSlider").GetComponent<UnityEngine.UI.Slider>();
        slider.value = dataPointIndex;
    }
    
    public void EqualizeTemperatures()
    {
        // add heat to rooms if the radiator is on
        foreach (Room room in rooms)
        {
            foreach (Radiator radiator in room.GetComponentsInChildren<Radiator>())
            {
                if (radiator != null && radiator.activated)
                {
                    room.roomTemperature += 1;
                }
            }
        }

        // equalise temperatures between each room and the outside weather
        foreach (Room room in rooms) {
            room.EqualiseTempToOutside();
        }

        // equalise temperatures between hallways
        foreach (Room room in rooms)
        {
            room.EqualiseTempToEasternHallway();
        }

        // equalise temperatures between rooms with a door inbetween
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

    public void addRoom(Room room)
    {
        rooms.Add(room);
    }

    public void addDoor(Door door)
    {
        doors.Add(door);
    }
}
