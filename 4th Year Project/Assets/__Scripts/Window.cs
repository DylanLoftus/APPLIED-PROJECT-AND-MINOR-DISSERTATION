using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    GameManager gameManager;
    Room room;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Window collision detected with player");

            GetComponentInChildren<Rigidbody>().transform.Rotate(90, 0, 0, Space.Self);
            gameManager.WindowOpen(room);

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Close window");

            GetComponentInChildren<Rigidbody>().transform.Rotate(-90, 0, 0, Space.Self);
            gameManager.windowOpen = false;
        }
    }
}
