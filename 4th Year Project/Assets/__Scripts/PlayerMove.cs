using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private string horizontalInput;

    [SerializeField]
    private string verticalInput;

    [SerializeField]
    private float movementSpeed;

    private CharacterController characterController;

    // Get a reference to the character controller.
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    // Moves the player.
    private void PlayerMovement()
    {
        // Get the horizontal and vertical axis.
        float vert = Input.GetAxis(verticalInput);
        float horiz = Input.GetAxis(horizontalInput);

        // Movement directions.
        Vector3 forwardMoving = transform.forward * vert;
        Vector3 rightMoving = transform.right * horiz;

        Vector3 movement = Vector3.ClampMagnitude(forwardMoving + rightMoving, 1) * movementSpeed;

        // Add the movement directions to the character controller.
        characterController.SimpleMove(movement);
    }
}
