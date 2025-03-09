using Unity.Cinemachine;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystemActions;
public class PlayerController : MonoBehaviour, IPlayerActions
{
    bool _isMoving;
    InputSystemActions _controls;
    Interactor _interactor;
    [SerializeField] CharacterController _body;
    [SerializeField] CinemachineCamera _cam;
    [field: SerializeField] public float InitialVelocity { get; private set; }
    [field: SerializeField] public float MaxVelocity { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    void Awake()
    {
        _controls = new InputSystemActions();
        _controls.Player.SetCallbacks(this);
        _interactor = new Interactor();
    }
    void OnEnable() => _controls.Player.Enable();
    void OnDisable() => _controls.Player.Disable();
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) _interactor.Interact();
    }
    void Update()
    {
        if (!_isMoving) return;
        _body.Move(MakeRelativeToCamera(GetMoveVector()) * InitialVelocity * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) _isMoving = true;
        else if (context.canceled) _isMoving = false;
    }
    Vector3 GetMoveVector() => _controls.Player.Move.ReadValue<Vector2>().ToVector3();
    Vector3 MakeRelativeToCamera(Vector3 vec) => Quaternion.Euler(0, _cam.transform.rotation.eulerAngles.y, 0) * vec;
}
