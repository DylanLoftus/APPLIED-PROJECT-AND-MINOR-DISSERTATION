using UnityEngine;

public class Radiator : MonoBehaviour
{
    public bool isOn;
    private bool playerNear;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerNear)
        {
            isOn = !isOn;
            mat.SetColor("_Color", isOn ? Color.red : Color.white);
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
