using UnityEngine;

public class Door : MonoBehaviour
{
    public Room[] connectingRooms;
    GameManager gameManager;
    public bool isOpen;
    private bool playerNear;
    public Transform cube;
    public GameObject cubeObject;

    [SerializeField]
    private AdjRooms adjRooms;
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cube = transform.GetChild(1);
        cubeObject = cube.gameObject;
        cubeObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerNear)
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                GetComponentInChildren<Rigidbody>().transform.Rotate(0, 90, 0, Space.Self);
                cubeObject.SetActive(false);
            }
            else
            {
                GetComponentInChildren<Rigidbody>().transform.Rotate(0, -90, 0, Space.Self);
                cubeObject.SetActive(true);
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

    public void EqualiseTempBetweenRooms()
    {
        Room[] twoRooms = adjRooms.GetConnectedRooms();
        Room roomA = twoRooms[0];
        Room roomB = twoRooms[1];

        // faster heat exchange if door is open
        float heatChangeRate = (isOpen ? 5 : 1) / 10f;

        float tempDiff = roomA.roomTemperature - roomB.roomTemperature;
        float tempChange = (tempDiff * heatChangeRate) / 2;
        roomA.roomTemperature -= tempChange;
        roomB.roomTemperature += tempChange;
    }
}
