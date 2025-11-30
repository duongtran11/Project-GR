using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SimpleAgentAimController : MonoBehaviour, IInputAxisOwner
{
    [Tooltip("Horizontal Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis HorizontalLook = new() { Range = new Vector2(-180, 180), Wrap = true, Recentering = InputAxis.RecenteringSettings.Default };

    [Tooltip("Vertical Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis VerticalLook = new() { Range = new Vector2(-70, 70), Recentering = InputAxis.RecenteringSettings.Default };
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(VerticalLook.Value, HorizontalLook.Value, 0f);
    }

    void IInputAxisOwner.GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref HorizontalLook, Name = "Horizontal Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.X });
        axes.Add(new() { DrivenAxis = () => ref VerticalLook, Name = "Vertical Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.Y });
    }
}
