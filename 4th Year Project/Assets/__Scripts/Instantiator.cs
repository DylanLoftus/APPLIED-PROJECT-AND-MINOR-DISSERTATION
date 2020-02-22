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

    private Transform roomSpawn;

    private int roomCount;
    private float rotation;
    private const int maxRoom = 2;
    private int hallwayLCount = -1;
    private const int MAXSPAWNROOMS = 8;
    private int totalRooms = 2;


    [SerializeField]
    private GameObject roomR;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;
    private GameObject sideWallS;
    private GameObject doorCover;

    public bool hallwayFull = true;
    private bool rotateHallway = false;
    public bool spawnLimit = false;

    private GameObject newRoom;
    private GameObject newHallway;
    private GameObject door;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        currentHallway = hallway;
    }

    private void CreateHallway(GameObject hallwayChoice)
    {
        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        wallDestroy = currentHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);
        
        // Set the hallway spawn locations and their respective room allocation.
        if(hallwayChoice == hallwayL)
        {
            hallwaySpawn = currentHallway.transform.Find("SpawnpointHallwayL").transform;
            hallwayLCount++;
            rotateHallway = true;
        }
        else
        {
            hallwaySpawn = currentHallway.transform.Find("SpawnpointHallway").transform;
            switch (hallwayLCount)
            {
                case 0:
                    rotation = 90;
                    break;
                case 1:
                    rotation = 180;
                    break;
                case 2:
                    rotation = 270;
                    break;

            }
        }
        Debug.Log("The current hallway is a: " + currentHallway.ToString());

        Debug.Log("Spawning in: " + hallwayChoice.ToString());
        newHallway = Instantiate(hallwayChoice, new Vector3(hallwaySpawn.transform.position.x, hallwaySpawn.transform.position.y, hallwaySpawn.transform.position.z), Quaternion.identity);
        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        if (rotateHallway && hallwayLCount > 0)
        {
            rotateHallway = false;
            rotation = currentHallway.transform.rotation.y + 90 * hallwayLCount;
            newHallway.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        }
        else
        {
            newHallway.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        }

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
    private GameObject CreateRoom(GameObject room, int rotation)
    {
        // Sets a room's rotation.
        switch (hallwayLCount)
        {
            case 0:
                if(GameObject.ReferenceEquals(room, roomL))
                {
                    rotation = 180;
                }
                else
                {
                    rotation = 0;
                }
                break;
            case 1:
                if (GameObject.ReferenceEquals(room, roomL))
                {
                    rotation = 270;
                }
                else
                {
                    rotation = 90;
                }
                break;
            case 2:
                if (GameObject.ReferenceEquals(room, roomL))
                {
                    rotation = 0;
                }
                else
                {
                    rotation = -180;
                }
                break;

        }

        // Set the room's spawn location.
        roomSpawn = currentHallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform;
        newRoom = Instantiate(roomR, new Vector3(roomSpawn.transform.position.x, roomSpawn.transform.position.y, roomSpawn.transform.position.z), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

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

        return newRoom;
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
                        roomL = CreateRoom(roomL, 90);
                        break;
                    case 2:
                        roomR = CreateRoom(roomR, -90);
                        break;
                }
            }
            /*
            Room oldRoom = roomL.GetComponent<Room>();
            roomL = CreateRoom(roomL, 90);

            // update room adjacencies
            Room newRoom = roomL.GetComponent<Room>();
            Room hallwayRoom = hallway.GetComponent<Room>();
            newRoom.adjRooms.roomSouth = hallwayRoom;
            hallwayRoom.adjRooms.roomNorth = newRoom;

            oldRoom.adjRooms.roomEast = newRoom;
            newRoom.adjRooms.roomWest = oldRoom;

            // update door adjacency
            Door newDoor = newHallway.transform.Find("SideWallN").GetComponentInChildren<Door>();

            gameManager.addDoor(newDoor);

            newDoor.adjRooms.roomSouth = hallwayRoom;
            newDoor.adjRooms.roomNorth = newRoom;

            // register new room with GameManager
            gameManager.addRoom(newRoom);
        }
        else
        {
            Room oldRoom = roomR.GetComponent<Room>();
            roomR = CreateRoom(roomR, -90);
            Room newRoom = roomR.GetComponent<Room>();
            doorCover.SetActive(false);
            door.SetActive(true);

            // update room adjacencies
            Room hallwayRoom = hallway.GetComponent<Room>();
            newRoom.adjRooms.roomNorth = hallwayRoom;
            hallwayRoom.adjRooms.roomSouth = newRoom;

            oldRoom.adjRooms.roomEast = newRoom;
            newRoom.adjRooms.roomWest = oldRoom;

            // update door adjacency
            Door newDoor = newHallway.transform.Find("SideWallS").GetComponentInChildren<Door>();
            gameManager.addDoor(newDoor);
            newDoor.adjRooms.roomNorth = hallwayRoom;
            newDoor.adjRooms.roomSouth = newRoom;

            // register new room with GameManager
            gameManager.addRoom(newRoom);
            */
        }
    }

}


