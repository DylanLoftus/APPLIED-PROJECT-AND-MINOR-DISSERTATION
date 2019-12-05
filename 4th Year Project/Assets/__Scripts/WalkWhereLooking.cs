using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWhereLooking : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private float moveSpeed = 2.5f;
    
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // move in the direction that the camera's facing
            rigidbody.velocity = cameraTransform.forward * moveSpeed;
        }
    }
}
