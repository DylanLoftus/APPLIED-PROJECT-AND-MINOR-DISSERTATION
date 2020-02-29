﻿using UnityEngine;

public class Door : Interactable
{
    public GameObject cube;
    
    public AdjRooms adjRooms;

    void Start()
    {
        cube = transform.GetChild(1).gameObject;
        cube.SetActive(true);
    }

    public void EqualiseTempBetweenRooms(float deltaMinutes)
    {
        Room[] twoRooms = adjRooms.GetConnectedRooms();
        Room roomA = twoRooms[0];
        Room roomB = twoRooms[1];

        // faster heat exchange if door is open
        float heatChangeRate = (activated ? 5 : 1) / 10f;

        // faster heat exchange if door is open, slower otherwise
        heatChangeRate = (activated ? 5 : 1) / 10f;

        float tempDiff = roomA.roomTemperature - roomB.roomTemperature;
        float tempChange = (tempDiff * heatChangeRate * deltaMinutes) / 2;
        roomA.roomTemperature -= tempChange;
        roomB.roomTemperature += tempChange;
    }

    public override void OnInteraction(bool activated)
    {
        GetComponentInChildren<Rigidbody>().transform.Rotate(0, activated ? 90 : -90, 0, Space.Self);
        cube.SetActive(!activated);
    }
}
