using UnityEngine;

public class Door : Interactable
{
    public GameObject cube;
    
    public AdjRooms adjRooms;

    void Awake()
    {
        cube = transform.GetChild(1).gameObject;
        cube.SetActive(true);
    }

    public void EqualiseTempBetweenRooms(float deltaMinutes)
    {
        Room[] twoRooms = adjRooms.GetConnectedRooms();
        Room roomA = twoRooms[0];
        Room roomB = twoRooms[1];

        // faster heat exchange if door is open
        float heatChangeRate = (activated ? 5 : 1) / 10f;

        // faster heat exchange if door is open, slower otherwise
        heatChangeRate = (activated ? 5 : 1) / 10f;

        float tempDiff = roomA.temperature - roomB.temperature;
        float tempChange = (tempDiff * heatChangeRate * deltaMinutes) / 2;
        roomA.temperature -= tempChange;
        roomB.temperature += tempChange;
    }

    public override void OnInteraction(bool activated)
    {
        GetComponentInChildren<Rigidbody>().transform.Rotate(0, activated ? 90 : -90, 0, Space.Self);
        cube.SetActive(!activated);
    }

    public override void ResetState()
    {
        GetComponentInChildren<Rigidbody>().transform.localRotation = Quaternion.Euler(0, 0, 0);
        cube.SetActive(true);
    }
}
