using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapRotation : MonoBehaviour
{
    GameObject player;
    GameObject miniMapCam;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        miniMapCam = GameObject.Find("MinimapCamera");
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z);

    }
}
