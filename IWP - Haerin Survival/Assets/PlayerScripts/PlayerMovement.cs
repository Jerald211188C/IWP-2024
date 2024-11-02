using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controller References")]
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private Transform _PlayerCamera;
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _Player;

    [Header("Player Variables")]
    [SerializeField] private float _Speed; // Base speed
    [SerializeField] private float _SprintMultiplier = 1.5f; // Sprint speed multiplier
    [SerializeField] private float _JumpForce;
    [SerializeField] private float _Gravity = -9.81f;

    private Vector3 _Velocity;
    private Vector3 _PlayerMovementInput;
    private Vector2 _PlayerMouseInput;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Gamemanager._instance._iswalking += Testing;
    }

    void Update()
    {
        _PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        RotatePlayer(); // Ensure the player rotates with the camera
    }

    private void MovePlayer()
    {
        // Get the camera's forward and right direction
        Vector3 forward = _PlayerCamera.transform.forward;
        Vector3 right = _PlayerCamera.transform.right;

        // Flatten the vectors to ignore the y-component
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors
        forward.Normalize();
        right.Normalize();

        // Calculate the desired move direction
        Vector3 moveDirection = (forward * _PlayerMovementInput.z + right * _PlayerMovementInput.x).normalized;

        // Set speed based on whether sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) ? _Speed * _SprintMultiplier : _Speed;

        if (_CharacterController.isGrounded)
        {
            _Velocity.y = -1f; // Reset the downward velocity when grounded

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Velocity.y = _JumpForce;
            }
        }
        else
        {
            _Velocity.y += _Gravity * Time.deltaTime; // Apply gravity
        }

        // Move the character controller
        _CharacterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        _CharacterController.Move(_Velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        // Only apply the yaw (horizontal rotation) from the camera to the player
        float playerYaw = _Camera.transform.eulerAngles.y;
        _Player.transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }

    private void Testing()
    {
        Debug.Log("I HAERIN");
    }

    private void OnDestroy()
    {
        if (Gamemanager._instance != null)
        {
            Gamemanager._instance._iswalking -= Testing; // Unsubscribe from the event
        }
    }
}
