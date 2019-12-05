using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiator : MonoBehaviour
{
    public bool isOn;

    GameManager gameManager;
    Room room;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();
        isOn = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (collision.tag == "Player")
            {
                isOn = !isOn;

                Debug.Log("Radiator on: " + isOn);
            }
        }
    }
}
