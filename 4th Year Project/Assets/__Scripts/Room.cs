﻿using UnityEngine;

public class Room : MonoBehaviour
{
    public float roomTemperature;

    private GameManager gameManager;
    private Room roomObject;
    private Window window;
    
    public AdjRooms adjRooms;
    
    public bool isHallway = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        roomObject = gameObject.GetComponent<Room>();
        window = gameObject.GetComponentInChildren<Window>();

        roomTemperature = 18;
    }
    
    void Update()
    {
        // change floor color of floor to gradient between blue/red based on temperature
        float floorTemp = 0;
        float ceilTemp = 18;
        float tempScale = ceilTemp - floorTemp;

        float tempGradient = Mathf.Clamp((roomTemperature - floorTemp) / tempScale, 0, 1);
        float red = tempGradient;
        float blue = (1 - tempGradient);
        
        var roomRenderer = roomObject.transform.Find("Floor").GetComponent<Renderer>();
        roomRenderer.material.SetColor("_Color", new Color(red, 0, blue));
    }

    public void EqualiseTempToOutside()
    {
        // count the number of walls that are exposed to the elements
        float numWallsExposed = adjRooms.NumSidesNotConnected();

        // compute heat rate to the outside (10x faster if window is open)
        float heatLossRate = (numWallsExposed * (window != null && window.activated ? 10 : 2)) / 80f;

        // change room temperature towards temperature outside
        float tempDiff = gameManager.outsideTemp - roomTemperature;
        float tempChange = tempDiff * heatLossRate;
        roomTemperature += tempChange;
    }

    public void EqualiseTempToAdjHallways()
    {
        if (!isHallway)
        {
            return;
        }

        foreach (Room adjRoom in adjRooms.GetConnectedRooms())
        {
            if (adjRoom.isHallway)
            {
                // fast temperature equalisation between hallways since they're
                // effectivly one big room
                // TODO this is duplicate code to code in Door.EqualiseTempBetweenRooms: create an abstraction
                float heatChangeRate = 0.9f;

                float tempDiff = roomTemperature - adjRoom.roomTemperature;
                float tempChange = (tempDiff * heatChangeRate) / 2;
                roomTemperature -= tempChange;
                adjRoom.roomTemperature += tempChange;
            }
        }
    }
}
