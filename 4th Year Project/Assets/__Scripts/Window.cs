using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isOpen;

    GameManager gameManager;
    Room room;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (isOpen)
            {
                Debug.Log("Close window");

                GetComponentInChildren<Rigidbody>().transform.Rotate(-90, 0, 0, Space.Self);
            }
            else
            {
                Debug.Log("Window collision detected with player");

                GetComponentInChildren<Rigidbody>().transform.Rotate(90, 0, 0, Space.Self);
            }

            isOpen = !isOpen;
        }
    }
}
