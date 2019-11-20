using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room[] connectingRooms;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Door collision detected with player");

            GetComponentInChildren<Rigidbody>().transform.Rotate(0, 90, 0, Space.Self);
            //gameManager.DoorOpen(connectingRooms);

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Close door");

            GetComponentInChildren<Rigidbody>().transform.Rotate(0, -90, 0, Space.Self);
        }
    }
}
