using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SimpleAgentAimController : MonoBehaviour, IInputAxisOwner
{
    public enum CouplingMode { Coupled, Decoupled }
    private SimpleAgentController _agentController;
    [Tooltip("Horizontal Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis HorizontalLook = new() { Range = new Vector2(-180, 180), Wrap = true, Recentering = InputAxis.RecenteringSettings.Default };

    [Tooltip("Vertical Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis VerticalLook = new() { Range = new Vector2(-70, 70), Recentering = InputAxis.RecenteringSettings.Default };
    public CouplingMode CameraCouplingMode;
    private Quaternion m_DesiredWorldRotation;

    void Start()
    {
        _agentController = GetComponentInParent<SimpleAgentController>();
    }

    void LateUpdate()
    {
        // m_DesiredWorldRotation = transform.rotation;
        transform.localRotation = Quaternion.Euler(VerticalLook.Value, HorizontalLook.Value, 0f);
        m_DesiredWorldRotation = transform.rotation;
        if (CameraCouplingMode == CouplingMode.Coupled)
        {
            Recenter();
        }
        else
        {
            // After player has been rotated, we subtract any rotation change
            // from our own transform, to maintain our world rotation
            transform.rotation = m_DesiredWorldRotation;
            var delta = (Quaternion.Inverse(_agentController.transform.rotation) * m_DesiredWorldRotation).eulerAngles;
            VerticalLook.Value = NormalizeAngle(delta.x);
            HorizontalLook.Value = NormalizeAngle(delta.y);
        }
    }

    public void Recenter()
    {
        var rot = transform.localRotation.eulerAngles;
        var delta = rot.y;
        _agentController.transform.rotation = Quaternion.AngleAxis(delta, _agentController.transform.up)
                                                * _agentController.transform.rotation;

        // Counter rotate
        HorizontalLook.Value -= delta;
        transform.rotation = Quaternion.Euler(rot);
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
