using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    // time constants
    private const float timeMultiplier = 720;
    private const float gameMinutesPerSecond = timeMultiplier / 60;
    private const float gameHoursPerSecond = gameMinutesPerSecond / 60;

    // gamification state
    public float money;
    private float playerComfort = 1.0f;

    // gamification variables
    private const float kwhRate = 0.150f;
    private float radiatorWattage = 300;
    private float radiatorTempIncreasePerMinute = 1;
    // radiators cut off after reaching a certain temperature (celsius)
    private float radiatorCutoffTemp = 20;

    public float timeStampForSun;
    
    void Start()
    {
        InitialiseRooms();
        
        // starting money
        money = 100;
    }

    void Update()
    {
        if (weatherHistory.length > 0)
        {
            float gameHours = Time.time * gameHoursPerSecond;
            int dataPointIndex = (int) (gameHours);
            // if simulation has ended, just use the last DataPoint
            dataPointIndex = Mathf.Min(dataPointIndex, weatherHistory.length - 1);
            DataPoint currDataPoint = weatherHistory.data[dataPointIndex];
            outsideTemp = currDataPoint.temperature;
            UpdateWeatherUI(currDataPoint.timestamp, dataPointIndex);

            float deltaMinutes = Time.deltaTime * gameMinutesPerSecond;
            // sun and moon logic
            timeStampForSun = float.Parse(currDataPoint.timestamp.Substring(12, 2));
            GameObject.Find("SunAndMoon").GetComponent<DayNightCycle>().RotateSunAndMoon(gameHours);
            //GameObject.Find("SunAndMoon").GetComponent<DayNightCycle>().SetSunMoonRotation(timeStampForSun);

            EqualizeTemperatures(deltaMinutes);
            // update gamification state
            GamificationStep(deltaMinutes);
            // update player stats on the UI
            UpdateStatsUI();
        }
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

    public void UpdateWeatherUI(string timestamp, int dataPointIndex)
    {
        TMPro.TextMeshProUGUI timestampComp = GameObject.FindGameObjectWithTag("WeatherTimestamp").GetComponent<TMPro.TextMeshProUGUI>();
        timestampComp.text = timestamp;

        TMPro.TextMeshProUGUI outsideTempComp = GameObject.FindGameObjectWithTag("OutsideTempText").GetComponent<TMPro.TextMeshProUGUI>();
        outsideTempComp.text = string.Format("It is {0}° outside", System.Math.Round(outsideTemp, 2));

        UnityEngine.UI.Slider slider = GameObject.FindGameObjectWithTag("ProgressSlider").GetComponent<UnityEngine.UI.Slider>();
        slider.value = dataPointIndex;
    }

    public void UpdateStatsUI()
    {
        TMPro.TextMeshProUGUI comfortComp = GameObject.FindGameObjectWithTag("ComfortValue").GetComponent<TMPro.TextMeshProUGUI>();
        comfortComp.text = string.Format("Comfort: {0}%", System.Math.Round(playerComfort * 100d, 1));

        TMPro.TextMeshProUGUI moneyComp = GameObject.FindGameObjectWithTag("PlayerMoney").GetComponent<TMPro.TextMeshProUGUI>();
        moneyComp.text = string.Format("Money: €{0}", System.Math.Round(money, 2));
    }

    public void GamificationStep(float deltaMinutes)
    {
        // find the room that the player is in
        // (if it's multiple, just use whichever room is listed first)
        Room playerRoom = null;
        foreach (Room room in rooms)
        {
            if (room.playerInside)
            {
                playerRoom = room;
                break;
            }
        }
        
        if (playerRoom != null)
        {
            float tempPlayerExperiencing = playerRoom.roomTemperature;
            float optimalTemp = 18;
            // degrees from optimalTemp where the player's comfort value reaches 0
            float zeroComfortOffset = 14;
            // degree range from offset where the player is fully comfortable
            float maxComfortRange = 1.5f;
            float offFromOptimal = Mathf.Abs(tempPlayerExperiencing - optimalTemp) - maxComfortRange;

            playerComfort = Mathf.Clamp(1 - (offFromOptimal / zeroComfortOffset), 0, 1);
        }
    }

    public void EqualizeTemperatures(float deltaMinutes)
    {
        // add heat to rooms if the radiator is on
        foreach (Room room in rooms)
        {
            foreach (Radiator radiator in room.GetComponentsInChildren<Radiator>())
            {
                if (radiator != null && radiator.activated)
                {
                    if (room.roomTemperature >= radiatorCutoffTemp)
                    {
                        // radiator has reached it's target temp; don't heat the room any more
                        // (color the radiator yellow to show this)
                        radiator.SetColor(Color.yellow);
                    }
                    else
                    {
                        room.roomTemperature += radiatorTempIncreasePerMinute * deltaMinutes;

                        // apply a monetary cost to the player for using up electricity
                        float kwhUsed = (radiatorWattage / 1000) / 60 * deltaMinutes;
                        float electricityCost = kwhUsed * kwhRate;
                        money -= electricityCost;

                        radiator.SetColor(Color.red);
                    }
                }
            }
        }

        // equalise temperatures between each room and the outside weather
        foreach (Room room in rooms) {
            room.EqualiseTempToOutside(deltaMinutes);
        }

        // equalise temperatures between hallways
        foreach (Room room in rooms)
        {
            room.EqualiseTempToEasternHallway(deltaMinutes);
        }

        // equalise temperatures between rooms with a door inbetween
        foreach (Door door in doors)
        {
            door.EqualiseTempBetweenRooms(deltaMinutes);
        }
    }
    
    public void SetWeatherData(WeatherHistory weatherHistory)
    {
        this.weatherHistory = weatherHistory;
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
