using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class FreeLook : MonoBehaviour
{
    CinemachineVirtualCamera Camera;

    [SerializeField]
    InputActionAsset Actions;

    [SerializeField]
    string CameraUpActionName = "Jump";

    [SerializeField]
    string CameraDownActionName = "Crouch";

    [SerializeField]
    string CameraMoveActionName = "Movement";

    [SerializeField]
    string CameraMouseLookActionName = "Mouse Look";

    [SerializeField]
    string CameraGamepadLookActionName = "Controller Look";

    [SerializeField]
    float MovementSpeed = 10f;

    [SerializeField]
    float TurnSpeed = 3f;

    [SerializeField]
    float VerticalMovementSpeed = 1f;

    [SerializeField]
    Transform SpawnAt;

    [SerializeField]
    bool CursorVisible = false;

    [SerializeField]
    bool LockCursor = true;

    float MovementInputH = 0f;
    float MovementInputV = 0f;
    float Yaw = 0f;
    float Pitch = 0f;
    float TurnSpeedH = 0f;
    float TurnSpeedV = 0f;
    bool MoveUp = false;
    bool MoveDown = false;
    InputAction MoveUpInputAction;
    InputAction MoveDownInputAction;

    private void Start()
    {
        // Camera setup
        Camera = GetComponent<CinemachineVirtualCamera>();
        if (Camera == null)
        {
            Debug.LogError("Camera was not found."); 
            Destroy(this);
        }

        // Init spawn position
        if (SpawnAt != null) transform.position = SpawnAt.position;

        // Cursor settings
        Cursor.visible = CursorVisible;
        if (LockCursor) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

        // Turn speed
        TurnSpeedH = TurnSpeed;
        TurnSpeedV = TurnSpeed;

        // Setup Up Input Actions
        MoveUpInputAction = Actions.FindAction(CameraUpActionName);

        if (MoveUpInputAction != null)
        {
            MoveUpInputAction.started += OnMoveUp;
            MoveUpInputAction.performed += OnMoveUp;
            MoveUpInputAction.canceled += OnMoveUp;
        }
        else
        {
            Debug.LogError("Up input action was not found.");
            Destroy(this);
        }

        // Setup Down Input Actions
        MoveDownInputAction = Actions.FindAction(CameraDownActionName);

        if (MoveDownInputAction != null)
        {
            MoveDownInputAction.started += OnMoveDown;
            MoveDownInputAction.performed += OnMoveDown;
            MoveDownInputAction.canceled += OnMoveDown;
        }
        else
        {
            Debug.LogError("Down input action was not found.");
            Destroy(this);
        }

        // Enable Actions
        Actions.Enable();
    }

    private void Update()
    {
        // Yaw
        if (Actions.FindAction(CameraMouseLookActionName).ReadValue<Vector2>().x != 0f) Yaw += TurnSpeedH * Actions.FindAction(CameraMouseLookActionName).ReadValue<Vector2>().x / 10f;
        else if (Actions.FindAction(CameraGamepadLookActionName).ReadValue<Vector2>().x != 0f) Yaw += TurnSpeedH * Actions.FindAction(CameraGamepadLookActionName).ReadValue<Vector2>().x;
        else Yaw += 0f;

        // Pitch
        if (Actions.FindAction(CameraMouseLookActionName).ReadValue<Vector2>().y != 0f) Pitch -= TurnSpeedV * Actions.FindAction(CameraMouseLookActionName).ReadValue<Vector2>().y / 10f;
        else if (Actions.FindAction(CameraGamepadLookActionName).ReadValue<Vector2>().y != 0f) Pitch -= TurnSpeedV * Actions.FindAction(CameraGamepadLookActionName).ReadValue<Vector2>().y;
        else Pitch -= 0f;

        // Euler Angles
        transform.eulerAngles = new Vector3(Pitch, Yaw, 0.0f);

        // Movement Vector
        MovementInputH = Actions.FindAction(CameraMoveActionName).ReadValue<Vector2>().y;
        MovementInputV = Actions.FindAction(CameraMoveActionName).ReadValue<Vector2>().x;
        Vector3 moveDirection = (transform.forward * MovementInputH + MovementInputV * transform.right).normalized;

        // Check Move Up/Down
        if(MoveUp) moveDirection.y += VerticalMovementSpeed;
        else if(MoveDown) moveDirection.y -= VerticalMovementSpeed;

        // Set position
        transform.position = transform.position + moveDirection * MovementSpeed * Time.deltaTime;
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) MoveUp = true;
        else MoveUp = false;
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) MoveDown = true;
        else MoveDown = false;
    }
}