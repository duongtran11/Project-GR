using UnityEngine;

public class AgentController : MonoBehaviour
{
    protected GameInput _controls;
    protected Animator _animator;
    protected Movement _movement;
    protected WeaponController _weaponController;
    [SerializeField] protected Transform _followTarget;
    [SerializeField] protected float _walkingSpeed;
    protected Vector2 _moveInput;
    protected Vector3 _moveDirection;
    public bool IsHandGun { get; set; }
    public bool IsMoving => _moveDirection.magnitude != 0;
    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponController = GetComponent<WeaponController>();
        _controls = new GameInput();
        _controls.Player.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Movement.canceled += ctx => _moveInput = Vector2.zero;
        _controls.Enable();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsHandGun = !IsHandGun;
            if (IsHandGun)
            {
                _weaponController.DrawRifle();
            }
            else
            {
                _weaponController.PutAwayRifle();
            }
            return;
        }
    }
    public Vector3 GetFollowPosition()
    {
        return _followTarget.position;
    }
}