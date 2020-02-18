using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllers : MonoBehaviour
{
    public Instantiator instantiator;
    [SerializeField]
    private GameObject hallChoice;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if ((OVRInput.GetDown(OVRInput.RawButton.A) == true) || (Input.GetKeyUp("space") == true)){
            hallChoice.SetActive(true);
        }
    }
}
