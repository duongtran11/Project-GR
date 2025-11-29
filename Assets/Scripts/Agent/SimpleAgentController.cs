using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SimpleAgentController : MonoBehaviour, IInputAxisOwner
{
    private GameInput _controls;
    [SerializeField]
    private float _walkingSpeed;
    private Animator _animator;
    private Vector2 _movement;
    private Vector3 _moveDirection;

    public InputAxis MoveX = InputAxis.DefaultMomentary;
    public InputAxis MoveZ = InputAxis.DefaultMomentary;
    public InputAxis Jump = InputAxis.DefaultMomentary;
    public InputAxis Sprint = InputAxis.DefaultMomentary;
    void Awake()
    {
        // _animator = GetComponent<Animator>();
        _controls = new GameInput();
        _controls.Player.Movement.performed += ctx => _movement = ctx.ReadValue<Vector2>();
        _controls.Player.Movement.canceled += ctx => _movement = Vector2.zero;
        _controls.Enable();
    }

    void Update()
    {
        HandleMovement();
        // _animator.SetFloat("walkingSpeed", _movement.magnitude);
    }

    private void HandleMovement()
    {
        _moveDirection = Vector3.ProjectOnPlane(transform.localRotation * new Vector3(_movement.x, 0, _movement.y)
                                , Vector3.up);
        transform.position += _walkingSpeed * Time.deltaTime * _moveDirection;
        if (_movement != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        }
    }

    void IInputAxisOwner.GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref MoveX, Name = "Move X", Hint = IInputAxisOwner.AxisDescriptor.Hints.X });
        axes.Add(new() { DrivenAxis = () => ref MoveZ, Name = "Move Z", Hint = IInputAxisOwner.AxisDescriptor.Hints.Y });
        axes.Add(new() { DrivenAxis = () => ref Jump, Name = "Jump" });
        axes.Add(new() { DrivenAxis = () => ref Sprint, Name = "Sprint" });
    }
}
