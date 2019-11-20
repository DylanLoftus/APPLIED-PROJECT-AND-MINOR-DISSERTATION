﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Room : MonoBehaviour
{
    public int roomId;
    public float roomTemperature;

    private GameManager gameManager;
    private Room roomObject;
    private Window window;

    [SerializeField]
    private Room roomNorth;
    [SerializeField]
    private Room roomSouth;
    [SerializeField]
    private Room roomEast;
    [SerializeField]
    private Room roomWest;

    private Room[] adjRooms;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        roomObject = GameObject.FindObjectOfType<Room>();
        window = GameObject.FindObjectOfType<Window>();

        adjRooms = new Room[] {roomNorth, roomSouth, roomEast, roomWest};
    }

    // Update is called once per frame
    void Update()
    {
        // change floor color of floor to gradient between blue/red based on temperature
        float floorTemp = 12;
        float ceilTemp = 16;
        float tempScale = ceilTemp - floorTemp;

        float tempGradient = Mathf.Clamp((gameManager.outsideTemp - floorTemp) / tempScale, 0, 1);
        float red = tempGradient;
        float blue = (1 - tempGradient);
        
        var roomRenderer = roomObject.GetComponentInChildren<Renderer>();
        roomRenderer.material.SetColor("_Color", new Color(red, 0, blue));
    }

    public void EqualizeTemperature()
    {
        //Debug.Log("Starting room temp: " + roomTemperature);
        //Debug.Log("outside temp: " + gameManager.outsideTemp);

        // count the number of walls that are exposed to the elements
        float numWallsExposed = adjRooms.Count(r => r == null);

        // compute heat rate to the outside (10x faster if window is open)
        float heatLossRate = (numWallsExposed * (window.isOpen ? 10 : 2)) / 20f;
        //Debug.Log("Heat loss rate: " + heatLossRate);

        // change room temperature towards temperature outside
        float tempDiff = gameManager.outsideTemp - roomTemperature;
        //Debug.Log("Temp diff: " + tempDiff);
        float tempChange = tempDiff * heatLossRate;
        //Debug.Log("temp change: " + tempChange);
        roomTemperature += tempChange;

        Debug.Log(gameObject.name + " temp: " + roomTemperature);
    }
}
