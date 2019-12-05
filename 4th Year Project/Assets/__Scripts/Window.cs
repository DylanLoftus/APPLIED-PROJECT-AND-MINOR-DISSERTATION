using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isOpen;

    GameManager gameManager;
    Room room;
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();
        isOpen = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (collision.tag == "Player")
            {
                if (isOpen)
                {
                    GetComponentInChildren<Rigidbody>().transform.Rotate(-90, 0, 0, Space.Self);
                }
                else
                {
                    GetComponentInChildren<Rigidbody>().transform.Rotate(90, 0, 0, Space.Self);
                }

                isOpen = !isOpen;
            }
        }
    }
}
