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

    private int turnCount = 0;


    [SerializeField]
    private GameObject roomR;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;
    private GameObject sideWallS;
    private GameObject doorCover;

    public bool rightRoomCreate = true;
    public bool leftRoomCreate = true;

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
        leftRoomCreate = false;
        rightRoomCreate = false;

        wallDestroy = hallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);
        
        if(hallwayChoice == hallwayL)
        {
            hallwaySpawn = hallway.transform.Find("SpawnpointHallwayL").transform;
        }
        else
        {
            hallwaySpawn = hallway.transform.Find("SpawnpointHallway").transform;
        }
        
        newHallway = Instantiate(hallwayChoice, new Vector3(hallwaySpawn.transform.position.x, hallwaySpawn.transform.position.y, hallwaySpawn.transform.position.z), Quaternion.identity);

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

    private GameObject CreateRoom(GameObject room, int rotation)
    {
        newRoom = Instantiate(roomR, new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z + 10), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        Room newRoomScript = newRoom.GetComponent<Room>();
        newRoomScript.adjRooms.reset();
        newRoomScript.roomTemperature = room.GetComponent<Room>().roomTemperature;

        return newRoom;
    }

    public void CheckRooms(int choice)
    {
        if (leftRoomCreate == true && rightRoomCreate == true)
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
            if (leftRoomCreate == false && rightRoomCreate == false)
            {
                Room oldRoom = roomL.GetComponent<Room>();
                roomL = CreateRoom(roomL, 90);
                leftRoomCreate = true;

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
                rightRoomCreate = true;

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
        }
    }

}


