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

    private Transform roomSpawn;

    private int turnCount = 0;
    private int roomCount;
    private int maxRoom;


    [SerializeField]
    private GameObject roomR;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;
    private GameObject sideWallS;
    private GameObject doorCover;

    public bool hallwayFull = true;

    private GameObject newRoom;
    private GameObject newHallway;
    private GameObject door;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void CreateHallway(GameObject hallwayChoice)
    {
        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        wallDestroy = hallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);
        
        // Set the hallway spawn locations and their respective room allocation.
        if(hallwayChoice == hallwayL)
        {
            hallwaySpawn = hallway.transform.Find("SpawnpointHallwayL").transform;
            maxRoom = 4;
        }
        else
        {
            hallwaySpawn = hallway.transform.Find("SpawnpointHallway").transform;
            maxRoom = 2;
        }
        
        newHallway = Instantiate(hallwayChoice, new Vector3(hallwaySpawn.transform.position.x, hallwaySpawn.transform.position.y, hallwaySpawn.transform.position.z), Quaternion.identity);

        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        if(hallwayChoice == hallwayL)
        {
            if(turnCount > 0)
            {
                newHallway.transform.Rotate(0, turnCount * 90, 0);
            }
            turnCount++;
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
        Room hallwayRoom = hallway.GetComponent<Room>();
        Room newHallwayRoom = newHallway.GetComponent<Room>();
        hallwayRoom.adjRooms.roomEast = newHallwayRoom;
        newHallwayRoom.adjRooms.roomWest = hallwayRoom;

        // register new room with GameManager
        gameManager.addRoom(hallwayRoom);

        hallway = newHallway;

        CheckRooms(0);
    }

    // Creates a room Game Object
    private GameObject CreateRoom(GameObject room, int rotation)
    {
        // Set the room's spawn location.
        roomSpawn = hallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform;
        newRoom = Instantiate(roomR, new Vector3(roomSpawn.transform.position.x, roomSpawn.transform.position.y, roomSpawn.transform.position.z), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        // Increment room count.
        roomCount++;

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
                        roomL = CreateRoom(roomL, -90);
                        break;
                    case 3:
                        roomL = CreateRoom(roomL, 180);
                        break;
                    case 4:
                        roomL = CreateRoom(roomL, 0);
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
            }
            */
        }
        else
        {
            Debug.Log("Invalid choice");
        }
    }

}


