using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OculusControllers : MonoBehaviour
{
    public Instantiator instantiator;
    [SerializeField]
    private GameObject hallChoice;
    [SerializeField]
    private Text warningText;

    // Start is called before the first frame update
    void Start()
    {
        warningText.gameObject.SetActive(false);
        warningText.text = "You have spanwed in the max ammount of rooms!";
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if ((OVRInput.GetDown(OVRInput.RawButton.A) == true) || (Input.GetKeyUp("space") == true))
        {
            if(instantiator.spawnLimit == false)
            {
                // If the hallway isn't full spawn in a room.
                if (instantiator.hallwayFull == false)
                {
                    instantiator.CheckRooms(0);
                }
                // If the hallway is full ask the user if they want another one.
                else if (instantiator.hallwayFull == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                    hallChoice.SetActive(true);
                }
            }
            else
            {
                StartCoroutine("RoomLimit");
            }

            
        }
    }

    private IEnumerator RoomLimit()
    {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        warningText.gameObject.SetActive(false);
    }
}
