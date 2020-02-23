using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject hallwayStraight;
    [SerializeField]
    private GameObject hallwayL;
    [SerializeField]
    private GameObject roomL;
    [SerializeField]
    private Transform hallwaySpawn;
    private GameObject currHallway;

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
    private GameObject door;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        currHallway = hallwayStraight;
    }

    private void CreateHallway(int hallwayChoice)
    {
        GameObject oldHallway = currHallway;

        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        wallDestroy = currHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);

        // Set the hallway spawn locations and their respective room allocation.
        hallwaySpawn = currHallway.transform.Find("SpawnpointHallway" + (hallwayChoice == 2 ? "L" : "")).transform;

        GameObject hallwayChoiceObject = (hallwayChoice == 1 ? hallwayStraight : hallwayL);
        currHallway = Instantiate(hallwayChoiceObject, new Vector3(hallwaySpawn.transform.position.x, hallwaySpawn.transform.position.y, hallwaySpawn.transform.position.z), Quaternion.identity);

        currHallway.transform.Rotate(0, turnCount * 90, 0);

        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        if (hallwayChoice == 2)
        {
            turnCount++;
        }

        wallInsDestroy = currHallway.transform.Find("WallInsDestroy").gameObject;
        wallInsDestroy.SetActive(false);

        wallDestroy = currHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(true);

        sideWallS = currHallway.transform.Find("SideWallS").gameObject;
        doorCover = sideWallS.transform.Find("DoorCover").gameObject;
        doorCover.SetActive(true);

        door = sideWallS.transform.Find("Door").gameObject;
        door.SetActive(false);

        // update room adjacencies
        Room oldHallwayRoom = oldHallway.GetComponent<Room>();
        Room currHallwayRoom = currHallway.GetComponent<Room>();
        oldHallwayRoom.adjRooms.roomEast = currHallwayRoom;
        currHallwayRoom.adjRooms.roomWest = oldHallwayRoom;

        // register new room with GameManager
        gameManager.addRoom(currHallwayRoom);

        CheckRooms(0);
    }

    // Creates a room Game Object
    private GameObject CreateRoom(GameObject room, int rotationOffset)
    {
        // Set the room's spawn location.
        roomSpawn = currHallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform;
        newRoom = Instantiate(roomR, new Vector3(roomSpawn.transform.position.x, roomSpawn.transform.position.y, roomSpawn.transform.position.z), Quaternion.identity);
        float roomRotation = currHallway.transform.rotation.eulerAngles.y + rotationOffset;
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, roomRotation, 0));

        // Increment room count.
        roomCount++;

        // If the room count exceeds the maximum number of rooms for a hallway the hallway is full.
        if (roomCount > 2)
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
        if (hallwayFull)
        {
            CreateHallway(choice);
        }
        else if (choice == 0)
        {
            // If the hallway is not full we have rooms to put in. Depending on how many rooms have already been created the rotations need to be set.
            // TODO: make rotation relative to hallway
            if (!hallwayFull)
            {
                switch (roomCount)
                {
                    case 1:
                        roomL = CreateRoom(roomL, 90);
                        break;
                    case 2:
                        roomL = CreateRoom(roomL, 180);
                        break;

                }
            }
        }
        else
        {
            Debug.Log("Invalid choice");
        }
    }

}


