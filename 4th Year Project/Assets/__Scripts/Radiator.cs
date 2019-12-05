using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiator : MonoBehaviour
{
    public bool isOn;

    GameManager gameManager;
    Room room;
    private Material mat;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        room = GameManager.FindObjectOfType<Room>();
        mat = gameObject.GetComponentInChildren<Renderer>().material;
        isOn = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (collision.tag == "Player")
            {
                isOn = !isOn;
                mat.SetColor("_Color", isOn ? Color.red : Color.white);
            }
        }
    }
}
