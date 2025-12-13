using System;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    protected GameInput _controls;
    protected Animator _animator;
    protected Movement _movement;
    protected PlayerWeapon _weapon;
    [SerializeField]
    protected float _walkingSpeed;
    protected Vector2 _moveInput;
    protected Vector3 _moveDirection;
    public bool IsHandGun { get; set; }
    public bool IsMoving => _moveDirection.magnitude != 0;
    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weapon = GetComponent<PlayerWeapon>();
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
                _weapon.DrawHandGun();
            }
            else
            {
                _weapon.PutAwayHandGun();
            }

            return;
        }
    }
}