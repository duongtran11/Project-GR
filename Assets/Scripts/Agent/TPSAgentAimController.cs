using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TPSAgentAimController : MonoBehaviour, IInputAxisOwner
{
    public enum CouplingMode { Coupled, Decoupled }
    [SerializeField]
    private TPSAgentController _agentController;
    [Tooltip("Horizontal Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis HorizontalLook = new() { Range = new Vector2(-180, 180), Wrap = true, Recentering = InputAxis.RecenteringSettings.Default };

    [Tooltip("Vertical Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis VerticalLook = new() { Range = new Vector2(-70, 70), Recentering = InputAxis.RecenteringSettings.Default };
    public CouplingMode CameraCouplingMode;

    void Start()
    {
        // _agentController = GetComponentInParent<SimpleAgentController>();
    }

    void LateUpdate()
    {
        transform.position = _agentController.transform.position;
        transform.rotation = Quaternion.Euler(VerticalLook.Value, HorizontalLook.Value, 0f);
        if (CameraCouplingMode == CouplingMode.Coupled)
        {
            Recenter();
        }
    }

    public void Recenter()
    {
        var rot = transform.rotation.eulerAngles;
        var delta = rot.y;
        _agentController.transform.rotation = Quaternion.AngleAxis(delta, _agentController.transform.up);
    }

    void IInputAxisOwner.GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref HorizontalLook, Name = "Horizontal Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.X });
        axes.Add(new() { DrivenAxis = () => ref VerticalLook, Name = "Vertical Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.Y });
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;
        return angle;
    }
}
