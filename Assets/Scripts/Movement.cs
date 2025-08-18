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
    private bool _isGrounded;
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var dir = transform.forward * verticalInput + transform.right * horizontalInput;

        _controller.Move(_moveSpeed * Time.deltaTime * dir);
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
}
