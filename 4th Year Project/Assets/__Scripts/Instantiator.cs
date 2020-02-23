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

    private void CreateHallway(GameObject hallwayChoice)
    {
        currentHallwayL = (hallwayChoice == hallwayL);

        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        wallDestroy = currentHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);

        // Set the hallway spawn locations and their respective room allocation.
        string spawnpointName = "SpawnpointHallway" + (currentHallwayL ? "L" : "");
        hallwaySpawn = currentHallway.transform.Find(spawnpointName).transform;
        Debug.Log("The current hallway is a: " + currentHallway.ToString());

        Debug.Log("Spawning in: " + hallwayChoice.ToString());
        Vector3 spawnPos = hallwaySpawn.transform.position;
        newHallway = Instantiate(hallwayChoice, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);
        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        rotation = currentHallway.transform.rotation.y + (90 * hallwayLCount);
        newHallway.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        wallInsDestroy = newHallway.transform.Find("WallInsDestroy").gameObject;
        wallInsDestroy.SetActive(false);

        wallDestroy = newHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(true);

        sideWallS = newHallway.transform.Find("SideWallS").gameObject;
        doorCover = sideWallS.transform.Find("DoorCover").gameObject;
        doorCover.SetActive(true);

        door = sideWallS.transform.Find("Door").gameObject;
        door.SetActive(false);

        // set up new spawnpoint for hallway
        hallwaySpawn = newHallway.transform.Find("SpawnpointHallway").transform;

        // update room adjacencies
        Room hallwayRoom = currentHallway.GetComponent<Room>();
        Room newHallwayRoom = newHallway.GetComponent<Room>();
        hallwayRoom.adjRooms.roomEast = newHallwayRoom;
        newHallwayRoom.adjRooms.roomWest = hallwayRoom;

        // register new room with GameManager
        gameManager.addRoom(hallwayRoom);

        currentHallway = newHallway;

        CheckRooms(0);
    }

    // Creates a room Game Object
    private void CreateRoom(GameObject room)
    {
        // Sets a room's rotation.
        //bool isLeftRoom = GameObject.ReferenceEquals(room, roomL);
        float roomRotation = room.transform.rotation.eulerAngles.y + (currentHallwayL ? 90 : 0) + rotation;
        
        // Set the room's spawn location.
        Vector3 roomSpawn = currentHallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform.position;
        newRoom = Instantiate(room, new Vector3(roomSpawn.x, roomSpawn.y, roomSpawn.z), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, roomRotation, 0));

        // Increment room count.
        roomCount++;
        // Increment total room count.
        totalRooms++;

        if (roomCount != 2)
        {
            sideWallS = newHallway.transform.Find("SideWallS").gameObject;
            doorCover = sideWallS.transform.Find("DoorCover").gameObject;
            doorCover.SetActive(false);

            door = sideWallS.transform.Find("Door").gameObject;
            door.SetActive(true);
        }

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

    public void CheckRooms(int choice)
    {
        Debug.Log(choice);

        // If all rooms have been filled create a new hallway.
        if (hallwayFull == true)
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
        else if (choice == 0)
        {
            // If the hallway is not full we have rooms to put in. Depending on how many rooms have already been created the rotations need to be set.
            // TODO: make rotation relative to hallway
            if (hallwayFull == false)
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
        }
    }

}


