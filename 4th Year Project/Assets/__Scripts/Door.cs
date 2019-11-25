using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room[] connectingRooms;
    GameManager gameManager;
    public bool isOpen;
    public Transform cube;
    public GameObject cubeObject;

    [SerializeField]
    private Room roomNorth;
    [SerializeField]
    private Room roomSouth;
    [SerializeField]
    private Room roomEast;
    [SerializeField]
    private Room roomWest;

    private AdjRooms adjRooms;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cube = transform.GetChild(1);
        cubeObject = cube.gameObject;
        cubeObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0) == true) {
            if (collision.tag == "Player")
            {
                if (isOpen)
                {
                    GetComponentInChildren<Rigidbody>().transform.Rotate(0, -90, 0, Space.Self);
                    cubeObject.SetActive(true);
                }
                else
                {
                    GetComponentInChildren<Rigidbody>().transform.Rotate(0, 90, 0, Space.Self);
                    //gameManager.DoorOpen(connectingRooms);
                    cubeObject.SetActive(false);
                }

                isOpen = !isOpen;
            }
        }
    }

    public void EqualiseTempBetweenRooms()
    {
        Room[] twoRooms = adjRooms.GetConnectedRooms();
        Room roomA = twoRooms[0];
        Room roomB = twoRooms[1];

        //Debug.Log("Starting room temp: " + roomTemperature);

        // faster heat exchange if door is open
        float heatChangeRate = (isOpen ? 5 : 1) / 20f;

        float tempDiff = roomA.roomTemperature - roomB.roomTemperature;
        float tempChange = (tempDiff * heatChangeRate) / 2;
        roomA.roomTemperature -= tempChange;
        roomB.roomTemperature += tempChange;
    }
}
