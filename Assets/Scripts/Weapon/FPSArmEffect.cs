using UnityEngine;
using UnityEngine.InputSystem;

public class FPSArmEffect : MonoBehaviour
{
    [SerializeField] private float _bobFrequency;
    [SerializeField] private float _bobAmplitude;
    [SerializeField] private float _swaySmooth;
    [SerializeField] private float _swayAmount;
    [SerializeField] private float _maxSwayAmount;
    private Vector3 _bobStartPos;
    private AgentController _agentController;
    private FPSCameraRig _cameraRig;

    void Awake()
    {
        _agentController = GetComponentInParent<AgentController>();
        _cameraRig = GetComponentInParent<FPSCameraRig>();
        _bobStartPos = transform.localPosition;
    }
    void Update()
    {
        HeadBob();
        Sway();
    }
    void HeadBob()
    {
        if (!_agentController.IsMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _bobStartPos, Time.deltaTime);
            return;
        }
        var pos = Vector3.zero;
        pos.y = Mathf.Lerp(pos.y, -Mathf.Sin(Time.time * _bobFrequency) * _bobAmplitude, Time.deltaTime);
        pos.x = Mathf.Lerp(pos.x, Mathf.Cos(Time.time * _bobFrequency / 2) * _bobAmplitude, Time.deltaTime);
        
        transform.localPosition += pos;
    }
    void Sway()
    {
        var mouseDelta = Mouse.current.delta.ReadValue();
        var horizontal = Mathf.Clamp(mouseDelta.x * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
        var vertical = Mathf.Clamp(mouseDelta.y * _swayAmount, -_maxSwayAmount, _maxSwayAmount);

        var rotX = Quaternion.AngleAxis(vertical, Vector3.right);
        var rotY = Quaternion.AngleAxis(horizontal, Vector3.up);

        var targetRotation = rotX * rotY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _swaySmooth);
    }
}
