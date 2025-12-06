using UnityEngine;

public class FPSAgentController : AgentController
{
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _moveDirection = transform.rotation * new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _walkingSpeed * Time.deltaTime * _moveDirection;
    }
}