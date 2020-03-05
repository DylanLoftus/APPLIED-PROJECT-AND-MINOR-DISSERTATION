using System;
using TMPro;
using UnityEngine;

public class TempDisplay : MonoBehaviour
{
    Room room;
    TextMeshPro currentRoomTemp;

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<Room>();
        currentRoomTemp = GetComponent<TextMeshPro>();
        currentRoomTemp.transform.rotation = Quaternion.identity;
        currentRoomTemp.transform.Rotate(90,0,90);
        currentRoomTemp.transform.position = new Vector3(room.transform.position.x, room.transform.position.y + 10, room.transform.position.z - (float)2.5);

    }

    // Update is called once per frame
    void Update()
    {
        float rounded = (float)Math.Round(room.temperature * 100f) / 100f;
        currentRoomTemp.text = rounded.ToString();
    }
}
