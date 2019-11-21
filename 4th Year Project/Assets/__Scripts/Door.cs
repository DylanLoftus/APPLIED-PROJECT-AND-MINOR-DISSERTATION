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
}
