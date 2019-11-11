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

        // change floor color of floor to gradient between blue/red based on temperature
        float floorTemp = 12;
        float ceilTemp = 16;
        float tempScale = ceilTemp - floorTemp;

        float tempGradient = Mathf.Clamp((gameManager.outsideTemp - floorTemp) / tempScale, 0, 1);
        float red = tempGradient;
        float blue = (1 - tempGradient);


        var roomRenderer = room.GetComponentInChildren<Renderer>();
        roomRenderer.material.SetColor("_Color", new Color(red, 0, blue));
    }


}
