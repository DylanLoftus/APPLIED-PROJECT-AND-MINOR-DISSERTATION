using UnityEngine;

public class RoomAdjUtils {
    // Initialise a room's adjacency list and temperature
    public static void InitialiseRoomTempLogic(GameObject room, GameObject copyTempRoom)
    {
        Room newRoomScript = room.GetComponent<Room>();
        newRoomScript.adjRooms.reset();
        newRoomScript.roomTemperature = copyTempRoom.GetComponent<Room>().roomTemperature;
    }

    // Create adjacencies between two rooms (north/south), for the temperature logic
    public static void LinkRoomsNorthSouth(GameManager gameManager, GameObject roomN, GameObject roomS, Transform doorWall)
    {
        // find scripts from GameObjects
        Room roomNorth = roomN.GetComponent<Room>();
        Room roomSouth = roomS.GetComponent<Room>();

        // create room adjacencies
        roomNorth.adjRooms.roomSouth = roomSouth;
        roomSouth.adjRooms.roomNorth = roomNorth;

        if (doorWall != null)
        {
            // create door room adjacencies
            Door door = doorWall.GetComponentInChildren<Door>();
            door.adjRooms.roomNorth = roomNorth;
            door.adjRooms.roomSouth = roomSouth;

            // register door with temperature logic
            gameManager.addDoor(door);
        }

        // register rooms with temperature logic
        gameManager.addRoom(roomNorth);
        gameManager.addRoom(roomSouth);
    }

    // Create adjacencies between two rooms (east/west), for the temperature logic
    public static void LinkRoomsEastWest(GameManager gameManager, GameObject roomE, GameObject roomW, Transform doorWall)
    {
        // find scripts from GameObjects
        Room roomEast = roomE.GetComponent<Room>();
        Room roomWest = roomW.GetComponent<Room>();

        // create room adjacencies
        roomEast.adjRooms.roomWest = roomWest;
        roomWest.adjRooms.roomEast = roomEast;

        if (doorWall != null)
        {
            // create door room adjacencies
            Door door = doorWall.GetComponentInChildren<Door>();
            door.adjRooms.roomEast = roomEast;
            door.adjRooms.roomWest = roomWest;

            // register door with temperature logic
            gameManager.addDoor(door);
        }

        // register rooms with temperature logic
        gameManager.addRoom(roomEast);
        gameManager.addRoom(roomWest);
    }
}
