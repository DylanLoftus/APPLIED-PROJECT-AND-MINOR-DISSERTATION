using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Room : MonoBehaviour
{
    public int roomId;
    public float roomTemperature;

    private GameManager gameManager;
    private Room roomObject;
    private Window window;
    
    public AdjRooms adjRooms;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        roomObject = gameObject.GetComponent<Room>();
        window = gameObject.GetComponentInChildren<Window>();

        // adjRooms = new Room[] {roomNorth, roomSouth, roomEast, roomWest};

        roomTemperature = 8;
    }

    // Update is called once per frame
    void Update()
    {
        // change floor color of floor to gradient between blue/red based on temperature
        float floorTemp = 8;
        float ceilTemp = 18;
        float tempScale = ceilTemp - floorTemp;

        float tempGradient = Mathf.Clamp((roomTemperature - floorTemp) / tempScale, 0, 1);
        float red = tempGradient;
        float blue = (1 - tempGradient);
        
        var roomRenderer = roomObject.GetComponentInChildren<Renderer>();
        roomRenderer.material.SetColor("_Color", new Color(red, 0, blue));
    }

    public void EqualiseTempToOutside()
    {
        //Debug.Log("Starting room temp: " + roomTemperature);
        //Debug.Log("outside temp: " + gameManager.outsideTemp);

        // count the number of walls that are exposed to the elements
        float numWallsExposed = adjRooms.NumSidesNotConnected();

        // compute heat rate to the outside (10x faster if window is open)
        float heatLossRate = (numWallsExposed * (window.isOpen ? 10 : 2)) / 80f;
        Debug.Log("Heat loss rate: " + heatLossRate);

        // change room temperature towards temperature outside
        float tempDiff = gameManager.outsideTemp - roomTemperature;
        Debug.Log("Temp diff: " + tempDiff);
        float tempChange = tempDiff * heatLossRate;
        Debug.Log("temp change: " + tempChange);
        roomTemperature += tempChange;

        //Debug.Log(gameObject.name + " temp: " + roomTemperature);
    }
}
