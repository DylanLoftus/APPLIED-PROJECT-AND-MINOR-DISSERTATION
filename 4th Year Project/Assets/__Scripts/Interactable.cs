using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool activated;
    private bool playerNear;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        activated = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerNear || (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) == true && playerNear))
        {
            activated = !activated;
            OnInteraction(activated);
        }
    }

    public abstract void OnInteraction(bool activated);

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
