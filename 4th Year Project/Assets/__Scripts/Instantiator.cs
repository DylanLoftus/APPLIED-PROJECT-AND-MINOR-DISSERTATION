using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject room;

    private GameObject wallInsDestroy;
    private GameObject wallDestroy;

    private GameObject newHallway;
    private GameObject newRoom;

    public Transform currentHallway;
    public Transform currentRoom;

    private GameObject previousHallway;

    private int multiplier = 1;
    private int i = 0;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentHallway.transform.position = hallway.transform.position;

        coroutine = WaitTime(2.0f);
        StartCoroutine(coroutine);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitTime(float waitTime)
    {
        while (i < 5)
        {
            yield return new WaitForSeconds(waitTime);

            i++;

            wallDestroy = hallway.transform.FindChild("WallDestroy").gameObject;
            wallDestroy.SetActive(false);

            newHallway = Instantiate(hallway, new Vector3(currentHallway.transform.position.x, currentHallway.transform.position.y, currentHallway.transform.position.z + 10 * multiplier), Quaternion.identity);

            wallInsDestroy = newHallway.transform.FindChild("WallInsDestroy").gameObject;
            wallInsDestroy.SetActive(false);

            wallDestroy = newHallway.transform.FindChild("WallDestroy").gameObject;
            wallDestroy.SetActive(true);

            if (i < 5)
            {
                wallDestroy.SetActive(false);
                wallInsDestroy.SetActive(false);
            }
            else
            {
                wallDestroy.SetActive(true);
            }

            if (i == 5)
            {
                wallDestroy.SetActive(true);
            }

            currentRoom.transform.position = room.transform.position;
            newRoom = Instantiate(room, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, currentRoom.transform.position.z + 10 * multiplier), Quaternion.identity);
            newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            multiplier++;
        }

}
}
