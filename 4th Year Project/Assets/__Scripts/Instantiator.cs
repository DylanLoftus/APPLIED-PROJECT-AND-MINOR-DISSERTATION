using UnityEngine;
using static RoomAdjUtils;

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
    private bool isCurrentHallwayL;

    private int roomCount;
    private float rotation;
    private const int maxRoom = 2;
    private int hallwayLCount = 0;
    private const int MAXSPAWNROOMS = 8;
    private int totalRooms = 2;

    [SerializeField]
    private GameObject roomR;

    private GameObject prevRoomL;
    private GameObject prevRoomR;

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
        isCurrentHallwayL = false;
        prevRoomL = roomL;
        prevRoomR = roomR;
    }

    // Creates a hallway Game Object.
    private void CreateHallway(GameObject hallwayChoice)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCurrentHallwayL = (hallwayChoice == hallwayL);

        // For creating rooms and checking if hallway is full.
        roomCount = 1;
        hallwayFull = false;

        // Taking the end off the hallway.
        wallDestroy = currentHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(false);

        // Set the hallway spawn locations and their respective room allocation.
        string spawnpointName = "SpawnpointHallway" + (isCurrentHallwayL ? "L" : "");
        hallwaySpawn = currentHallway.transform.Find(spawnpointName).transform;

        Vector3 spawnPos = hallwaySpawn.transform.position;
        newHallway = Instantiate(hallwayChoice, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);
        InitialiseRoomTempLogic(newHallway, currentHallway);

        // reset hallway (close doors)
        newHallway.GetComponent<Room>().ResetState();

        // If the hallway is an L hallway we'll need to rotate the second one and ever subsequent one after that 90 degrees.
        rotation = currentHallway.transform.rotation.y + (90 * hallwayLCount);
        newHallway.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        // Take the front off of the next hallway.
        wallInsDestroy = newHallway.transform.Find("WallInsDestroy").gameObject;
        wallInsDestroy.SetActive(false);

        // Put the back on it.
        wallDestroy = newHallway.transform.Find("WallDestroy").gameObject;
        wallDestroy.SetActive(true);

        // set up new spawnpoint for hallway
        hallwaySpawn = newHallway.transform.Find("SpawnpointHallway").transform;

        // update room adjacencies
        LinkRoomsEastWest(gameManager, currentHallway, newHallway, null);

        // register new room with GameManager
        gameManager.addRoom(newHallway.GetComponent<Room>());

        // Set the newHallway as the currentHallway.
        currentHallway = newHallway;

        // Spawn in a room.
        CheckRooms(0);
    }

    // Creates a room Game Object.
    private GameObject CreateRoom(GameObject roomTemplate)
    {
        // Sets a room's rotation.
        float roomRotation = roomTemplate.transform.rotation.eulerAngles.y + (isCurrentHallwayL ? 90 : 0) + rotation;

        // Set the room's spawn location and instantiate it.
        Vector3 roomSpawn = currentHallway.transform.Find("SpawnpointRoom" + roomCount.ToString()).transform.position;
        newRoom = Instantiate(roomTemplate, new Vector3(roomSpawn.x, roomSpawn.y, roomSpawn.z), Quaternion.identity);
        newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, roomRotation, 0));

        // reset room state (close window, turn off radiators, etc)
        newRoom.GetComponent<Room>().ResetState();

        // Link new room up for the temperature exchange logic
        LinkNewRoom();

        // Increment room count.
        roomCount++;
        // Increment total room count.
        totalRooms++;

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

        return newRoom;
    }

    // Link new room up for the temperature exchange logic
    private void LinkNewRoom()
    {
        // temperature logic setup for this room
        InitialiseRoomTempLogic(newRoom, currentHallway);
        if (roomCount == 1)
        {
            // north/south room adjacency
            LinkRoomsNorthSouth(gameManager, newRoom, currentHallway, currentHallway.transform.Find("SideWallN"));

            if (!isCurrentHallwayL)
            {
                // east/west adjacency with previous L room
                LinkRoomsEastWest(gameManager, newRoom, prevRoomL, null);
            }
        }
        else
        {
            // south/north room adjacency
            LinkRoomsNorthSouth(gameManager, currentHallway, newRoom, currentHallway.transform.Find("SideWallS"));

            if (!isCurrentHallwayL)
            {
                // east/west adjacency with previous L room
                LinkRoomsEastWest(gameManager, newRoom, prevRoomR, null);
            }
        }
    }

    // Checks to see if a new hallway needs to be made or if a room needs to be made.
    public void CheckRooms(int choice)
    {
        // If all rooms have been filled create a new hallway.
        if (hallwayFull)
        {
            PickHallway(choice);
        }
        else if (choice == 0)
        {
            // If the hallway is not full we have rooms to put in. Depending on how many rooms have already been created the rotations need to be set.
            PickRoom(roomCount);
        }
    }

    // Picks the hallway type and calls the CreateHallway method.
    private void PickHallway(int choice)
    {
        if (choice == 1)
        {
            CreateHallway(hallway);
        }
        else
        {
            CreateHallway(hallwayL);
            hallwayLCount++;
        }
    }

    // Picks which room type and calls the CreateRoom method.
    private void PickRoom(int roomCount)
    {
        if (roomCount == 1)
        {
            SetDoorwayHidden(true);
            prevRoomL = CreateRoom(roomL);
        }
        else
        {
            SetDoorwayHidden(false);
            prevRoomR = CreateRoom(roomR);
        }
    }

    // Reveals/hides a doorway.
    private void SetDoorwayHidden(bool hidden)
    {
        sideWallS = newHallway.transform.Find("SideWallS").gameObject;
        doorCover = sideWallS.transform.Find("DoorCover").gameObject;
        door = sideWallS.transform.Find("Door").gameObject;

        doorCover.SetActive(hidden);
        door.SetActive(!hidden);
    }
}


