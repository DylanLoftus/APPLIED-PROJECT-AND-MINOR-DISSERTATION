using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AdjRooms
{
    public Room roomNorth;
    public Room roomSouth;
    public Room roomEast;
    public Room roomWest;

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

    public void reset()
    {
        roomNorth = roomSouth = roomEast = roomWest = null;
    }
}