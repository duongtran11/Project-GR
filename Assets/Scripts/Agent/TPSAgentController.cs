using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TPSAgentController : AgentController, IInputAxisOwner
{
    [SerializeField]
    private TPSAgentAimController _aimController;
    public InputAxis MoveX = InputAxis.DefaultMomentary;
    public InputAxis MoveZ = InputAxis.DefaultMomentary;
    public InputAxis Jump = InputAxis.DefaultMomentary;
    public InputAxis Sprint = InputAxis.DefaultMomentary;


    void Update()
    {
        HandleMovement();
        // _animator.SetFloat("walkingSpeed", _movement.magnitude);
    }

    private void HandleMovement()
    {
        var yaw = Quaternion.Euler(0f, _aimController.transform.eulerAngles.y, 0f);
        _moveDirection = yaw * new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _walkingSpeed * Time.deltaTime * _moveDirection;
        if (_moveInput != Vector2.zero)
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
