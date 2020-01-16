using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isOpen;
    private bool playerNear;

    GameManager gameManager;
    Room room;
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerNear)
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                GetComponentInChildren<Rigidbody>().transform.Rotate(90, 0, 0, Space.Self);
            }
            else
            {
                GetComponentInChildren<Rigidbody>().transform.Rotate(-90, 0, 0, Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            playerNear = false;
        }
    }
}
