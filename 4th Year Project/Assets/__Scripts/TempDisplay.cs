using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TempDisplay : MonoBehaviour
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
        float rounded = (float)Math.Round(room.roomTemperature * 100f) / 100f;
        currentRoomTemp.text = rounded.ToString();
    }
}
