using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private string mouseXInput;

    [SerializeField]
    private string mouseYInput;

    [SerializeField]
    private float mouseSensitivity;

    [SerializeField]
    private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        //LockCursor();
        xAxisClamp = 0.0f;
    }

    // Locks the cursor so it doesn't appear on screen.
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    // Gets the mouse x and y axis and roates the camera in the direction the mouse is being moved and at a
    // set sensitivity or speed.
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInput) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInput) * mouseSensitivity * Time.deltaTime;

        // Monitors the rotation degrees.
        xAxisClamp += mouseY;

        // Stops the player from looking upsdie down.
        if(xAxisClamp > 90.0f)
        {
            // We are looking directly upwards.
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxis(270.0f);
        }
        else if(xAxisClamp < -90.0f)
        {
            // We are looking directly down.
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxis(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Stops the camera rotaion going past the clamp value.
    private void ClampXAxis(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
