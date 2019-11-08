using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Room : MonoBehaviour
{
    public int roomId;
    public float roomTemperature;

    GameManager gameManager;
    Room room;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameObject.FindObjectOfType<Room>();

    }

    // Update is called once per frame
    void Update()
    {
        roomTemperature = gameManager.outsideTemp + 8;

        if(gameManager.outsideTemp > 15.0)
        {
            // Change colour of walls to blue
            var roomRenderer = room.GetComponentInChildren<Renderer>();
            roomRenderer.material.SetColor("_Color", Color.blue);

        }
        else
        {
            // Change colour of walls to red
            var roomRenderer = room.GetComponentInChildren<Renderer>();
            roomRenderer.material.SetColor("_Color", Color.red);
        }
    }


}
