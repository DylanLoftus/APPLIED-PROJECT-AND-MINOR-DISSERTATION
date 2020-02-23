using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject hallwayL;
    [SerializeField]
    private GameObject roomL;
    [SerializeField]
    private Transform hallwaySpawn;
    private GameObject currentHallway;
    private bool currentHallwayL;

    private int roomCount;
    private float rotation;
    private const int maxRoom = 2;
    private int hallwayLCount = 0;
    private const int MAXSPAWNROOMS = 8;
    private int totalRooms = 2;

    [SerializeField]
    private GameObject roomR;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;
    private GameObject sideWallS;
    private GameObject doorCover;

    public bool hallwayFull = true;
    public bool spawnLimit = false;

    private GameObject newRoom;
    private GameObject newHallway;
    private GameObject door;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        currentHallway = hallway;
        currentHallwayL = false;
    }

    // Creates a hallway Game Object.
    private void CreateHallway(GameObject hallwayChoice)
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentHallwayL = (hallwayChoice == hallwayL);

        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        // Taking the end off the hallway.
        wallDestroy = currentHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);

        // Set the hallway spawn locations and their respective room allocation.
        string spawnpointName = "SpawnpointHallway" + (currentHallwayL ? "L" : "");
        hallwaySpawn = currentHallway.transform.Find(spawnpointName).transform;

        Vector3 spawnPos = hallwaySpawn.transform.position;
        newHallway = Instantiate(hallwayChoice, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);

        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        rotation = currentHallway.transform.rotation.y + (90 * hallwayLCount);
        newHallway.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        // Take the front off of the next hallway.
        wallInsDestroy = newHallway.transform.Find("WallInsDestroy").gameObject;
        wallInsDestroy.SetActive(false);

        // Put the back on it.
        wallDestroy = newHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(true);

        // Hide the doorway.
        ShowHideDoorway("hide");

        // set up new spawnpoint for hallway
        hallwaySpawn = newHallway.transform.Find("SpawnpointHallway").transform;

        // update room adjacencies
        Room hallwayRoom = currentHallway.GetComponent<Room>();
        Room newHallwayRoom = newHallway.GetComponent<Room>();
        hallwayRoom.adjRooms.roomEast = newHallwayRoom;
        newHallwayRoom.adjRooms.roomWest = hallwayRoom;

        // register new room with GameManager
        gameManager.addRoom(hallwayRoom);

        // Set the newHallway as the currentHallway.
        currentHallway = newHallway;

        // Spawn in a room.
        CheckRooms(0);
    }

    // Creates a room Game Object.
    private void CreateRoom(GameObject room)
    {
        // Sets a room's rotation.
        //bool isLeftRoom = GameObject.ReferenceEquals(room, roomL);
        float roomRotation = room.transform.rotation.eulerAngles.y + (currentHallwayL ? 90 : 0) + rotation;
        
        // Set the room's spawn location and instantiate it.
        Vector3 roomSpawn = currentHallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform.position;
        newRoom = Instantiate(room, new Vector3(roomSpawn.x, roomSpawn.y, roomSpawn.z), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, roomRotation, 0));

        // Increment room count.
        roomCount++;
        // Increment total room count.
        totalRooms++;

        // If the second room has not been spawned in yet hide the door.
        if (roomCount != 2)
        {
            ShowHideDoorway("show");
        }
        
        // If the total amount of rooms spawned in is equal to the limit. Set spawnLimit to true.
        if (totalRooms == MAXSPAWNROOMS)
        {
            spawnLimit = true;
        }

        // If the room count exceeds the maximum number of rooms for a hallway the hallway is full.
        if (roomCount > maxRoom)
        {
            hallwayFull = true;
        }

        Room newRoomScript = newRoom.GetComponent<Room>();
        newRoomScript.adjRooms.reset();
        newRoomScript.roomTemperature = room.GetComponent<Room>().roomTemperature;
    }

    // Checks to see if a new hallway needs to be made or if a room needs to be made.
    public void CheckRooms(int choice)
    {
        // If all rooms have been filled create a new hallway.
        if (hallwayFull == true)
        {
            PickHallway(choice);
        }
        else if (choice == 0)
        {
            // If the hallway is not full we have rooms to put in. Depending on how many rooms have already been created the rotations need to be set.
            if (hallwayFull == false)
            {
                PickRoom(roomCount);
            }
        }
    }

    // Picks the hallway type and calls the CreateHallway method.
    private void PickHallway(int choice)
    {
        if (choice == 1)
        {
            CreateHallway(hallway);
        }
        else if (choice == 2)
        {
            CreateHallway(hallwayL);
            hallwayLCount++;
        }
    }

    // Picks which room type and calls the CreateRoom method.
    private void PickRoom(int roomCount)
    {
        switch (roomCount)
        {
            case 1:
                CreateRoom(roomL);
                break;
            case 2:
                CreateRoom(roomR);
                break;
        }
    }

    // Reveals/hides a doorway.
    private void ShowHideDoorway(string showHide)
    {
        sideWallS = newHallway.transform.Find("SideWallS").gameObject;
        doorCover = sideWallS.transform.Find("DoorCover").gameObject;
        door = sideWallS.transform.Find("Door").gameObject;
        if (showHide == "show")
        {
            doorCover.SetActive(false);
            door.SetActive(true);
        }
        else
        {
            doorCover.SetActive(true);
            door.SetActive(false);
        }
    }

}


