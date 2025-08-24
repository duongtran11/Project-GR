using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private float _gravity = -9.81f;
    private Vector3 _velocity;
    public float _horizontalInput;
    public float _verticalInput;
    public Vector3 _moveDirection;
    private bool _isWalking;
    private bool _isRunning;
    private bool _isCrouching;
    private float _currentHInput;
    private float _currentVInput;

    public StateMachine<Movement> MovementSM = new();
    public StateFactory<Movement> MovementSF = new();
    public Animator Anim;
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;
    public float CrouchSpeed = 1f;
    public float BackwardWalkSpeed = 1f;
    public float BackwardRunSpeed = 2f;
    public float BackwardCrouchSpeed = 1f;
    public float HorizontalInput => _horizontalInput;
    public float VerticalInput => _verticalInput;
    public Vector3 MoveDirection => _moveDirection;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set { _moveSpeed = value; }
    }
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        var idleState = MovementSF.GetOrCreate<IdleState>(this);
        MovementSM.ChangeState(idleState);
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _moveDirection = transform.forward * _currentVInput + transform.right * _currentHInput;

        _currentHInput = Mathf.MoveTowards(_currentHInput, _horizontalInput, Time.deltaTime * 2f);
        _currentVInput = Mathf.MoveTowards(_currentVInput, _verticalInput, Time.deltaTime * 2f);
        MovementSM.CurrentState.Update();
        Anim.SetFloat("HInput", _currentHInput);
        Anim.SetFloat("VInput", _currentVInput);
        ApplyGravity();
    }
    void ApplyGravity()
    {
        if (!DetectGround())
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0f;
        }
        _controller.Move(_velocity * Time.deltaTime);
    }
    bool DetectGround()
    {
        var radius = 0.05f;
        if (Physics.CheckSphere(transform.position, radius, _groundMask))
        {
            return true;
        }
        return false;
    }
    void OnAnimatorMove()
    {
        Vector3 velocity = Anim.deltaPosition;
        _controller.Move(velocity);
    }
}
