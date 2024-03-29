﻿using UnityEngine;

public class Room : MonoBehaviour
{
    public float temperature = 0;

    private GameManager gameManager;
    private Room roomObject;
    private Window window;
    
    public AdjRooms adjRooms;
    
    public bool isHallway = false;
    public bool playerInside;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        roomObject = gameObject.GetComponent<Room>();
        window = gameObject.GetComponentInChildren<Window>();

        if (temperature == 0)
        {
            // default room temperature
            temperature = 18;
        }
    }
    
    void Update()
    {
        // change floor color of floor to gradient between blue/red based on temperature
        float intensity = 0.5f;
        float floorTemp = 8;
        float ceilTemp = 25;
        float tempScale = ceilTemp - floorTemp;

        float tempGradient = Mathf.Clamp((temperature - floorTemp) / tempScale, 0, 1);
        float red = tempGradient;
        float blue = (1 - tempGradient);

        // intensity modifier
        red *= intensity;
        blue *= intensity;
        
        // floor may be split into multiple parts
        Renderer[] floorRenderers = roomObject.transform.Find("Floor").GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in floorRenderers)
        {
            renderer.material.SetColor("_Color", new Color(red, 0, blue));
        }
    }

    public void EqualiseTempToOutside(float deltaMinutes)
    {
        // count the number of walls that are exposed to the elements
        float numWallsExposed = adjRooms.NumSidesNotConnected();

        // compute heat rate to the outside (10x faster if window is open)
        float heatLossRate = (numWallsExposed * (window != null && window.activated ? 10 : 2)) / 400f;

        // change room temperature towards temperature outside
        float tempDiff = gameManager.outsideTemp - temperature;
        float tempChange = tempDiff * heatLossRate * deltaMinutes;
        temperature += tempChange;
    }

    public void EqualiseTempToEasternHallway(float deltaMinutes)
    {
        if (!isHallway)
        {
            return;
        }

        Room eastRoom = adjRooms.roomEast;
        if (eastRoom != null && eastRoom.isHallway)
        {
            // fast temperature equalisation between hallways since they're
            // effectivly one big room
            // TODO this is duplicate code to code in Door.EqualiseTempBetweenRooms: create an abstraction
            float heatChangeRate = 0.9f;

            float tempDiff = temperature - eastRoom.temperature;
            float tempChange = (tempDiff * heatChangeRate * deltaMinutes) / 2;
            temperature -= tempChange;
            eastRoom.temperature += tempChange;
        }
    }

    public float SimulateRadiators(float deltaMinutes)
    {
        float totalKwh = 0;
        foreach (Radiator radiator in GetComponentsInChildren<Radiator>())
        {
            // simulate radiator for the given amount of time (returns kwh used)
            float kwhUsed = radiator.SimulateTime(deltaMinutes, this);
            // add this radiator's kwh usage to the total
            totalKwh += kwhUsed;
        }

        return totalKwh;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = false;
        }
    }

    public void ResetState()
    {
        // deactivate and reset all doors, radiators, and windows
        foreach (Interactable interactable in GetComponentsInChildren<Interactable>())
        {
            interactable.activated = false;
            interactable.ResetState();
        }
    }
}
