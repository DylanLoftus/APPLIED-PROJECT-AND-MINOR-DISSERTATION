using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject roomL;

    [SerializeField]
    private GameObject roomR;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;
    private GameObject sideWallS;
    private GameObject doorCover;

    private bool rightRoomCreate = true;
    private bool leftRoomCreate = true;

    private GameObject newRoom;
    private GameObject newHallway;
    private GameObject door;

    private Door newDoorN, newDoorS;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void CreateHallway()
    {
        leftRoomCreate = false;
        rightRoomCreate = false;

        wallDestroy = hallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);

        newHallway = Instantiate(hallway, new Vector3(hallway.transform.position.x, hallway.transform.position.y, hallway.transform.position.z + 10), Quaternion.identity);

        wallInsDestroy = newHallway.transform.Find("WallInsDestroy").gameObject;
        wallInsDestroy.SetActive(false);

        wallDestroy = newHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(true);

        sideWallS = newHallway.transform.Find("SideWallS").gameObject;
        doorCover = sideWallS.transform.Find("DoorCover").gameObject;
        doorCover.SetActive(true);

        door = sideWallS.transform.Find("Door").gameObject;
        door.SetActive(false);

        // update room adjacencies
        Room hallwayRoom = hallway.GetComponent<Room>();
        Room newHallwayRoom = newHallway.GetComponent<Room>();
        hallwayRoom.adjRooms.roomEast = newHallwayRoom;
        newHallwayRoom.adjRooms.roomWest = hallwayRoom;

        // register new rooms and doors with GameManager
        gameManager.addRoom(hallwayRoom);
        gameManager.addDoor(door.GetComponent<Door>());
        gameManager.addDoor(door.GetComponent<Door>());

        newDoorN = hallway.transform.Find("SideWallN").GetComponent<Door>();
        newDoorS = hallway.transform.Find("SideWallS").GetComponent<Door>();

        gameManager.addDoor(newDoorN);
        gameManager.addDoor(newDoorS);

        newDoorN.adjRooms.roomSouth = newHallwayRoom;
        newDoorS.adjRooms.roomNorth = newHallwayRoom;

        hallway = newHallway;

        CheckRooms();
    }

    private GameObject CreateRoom(GameObject room, int rotation)
    {
        newRoom = Instantiate(roomR, new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z + 10), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        return newRoom;
    }

    public void CheckRooms()
    {
        if(leftRoomCreate == true && rightRoomCreate == true)
        {
            CreateHallway();
        }
        else if (leftRoomCreate == false && rightRoomCreate == false)
        {
            roomL = CreateRoom(roomL, 90);
            leftRoomCreate = true;

            // update room adjacencies
            Room newRoom = roomL.GetComponent<Room>();
            Room hallwayRoom = hallway.GetComponent<Room>();
            newRoom.adjRooms.roomSouth = hallwayRoom;
            hallwayRoom.adjRooms.roomNorth = newRoom;

            // update door adjacency
            newDoorN.adjRooms.roomNorth = newRoom;
        }
        else
        {
            roomR = CreateRoom(roomR, -90);
            doorCover.SetActive(false);
            door.SetActive(true);
            rightRoomCreate = true;

            // update room adjacencies
            Room newRoom = roomR.GetComponent<Room>();
            Room hallwayRoom = hallway.GetComponent<Room>();
            newRoom.adjRooms.roomNorth = hallwayRoom;
            hallwayRoom.adjRooms.roomSouth = newRoom;

            // update door adjacency
            newDoorS.adjRooms.roomSouth = newRoom;
        }
    }

}
