using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AdjRooms
{
    [SerializeField]
    private Room roomNorth;
    [SerializeField]
    private Room roomSouth;
    [SerializeField]
    private Room roomEast;
    [SerializeField]
    private Room roomWest;

    public Room[] GetAdjList()
    {
        return new Room[] { roomNorth, roomSouth, roomEast, roomWest };
    }

    public Room[] GetConnectedRooms()
    {
        return GetAdjList().Where(r => r != null).ToArray();
    }

    public int NumConnectedRooms()
    {
        return GetAdjList().Count(r => r != null);
    }

    public int NumSidesNotConnected()
    {
        return GetAdjList().Count(r => r == null);
    }
}