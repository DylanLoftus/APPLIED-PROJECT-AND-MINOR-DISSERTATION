using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempDisplayRoom : MonoBehaviour
{
    Room room;
    TextMeshPro currentRoomTemp;

    // Start is called before the first frame update
    void Start()
    {

        room = GetComponentInParent<Room>();
        currentRoomTemp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float rounded = (float)Math.Round(room.temperature * 100f) / 100f;
        currentRoomTemp.text = rounded.ToString();
    }
}
