using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class FPSCameraRig : MonoBehaviour, IInputAxisOwner
{
    [SerializeField]
    private FPSAgentController _agentController;
    public InputAxis HorizontalLook = new() { Range = new Vector2(-180f, 180f), Wrap = true, Recentering = InputAxis.RecenteringSettings.Default };
    public InputAxis VerticalLook = new() { Range = new Vector2(-60f, 60f), Recentering = InputAxis.RecenteringSettings.Default };

    public void GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new()
        {
            DrivenAxis = () => ref HorizontalLook,
            Name = "Horizontal Look",
            Hint = IInputAxisOwner.AxisDescriptor.Hints.X
        });
        axes.Add(new()
        {
            DrivenAxis = () => ref VerticalLook,
            Name = "Vertical Look",
            Hint = IInputAxisOwner.AxisDescriptor.Hints.Y
        });
    }

    void Update()
    {
        _agentController.transform.rotation = Quaternion.Euler(0f, HorizontalLook.Value, 0f);
        transform.localRotation = Quaternion.Euler(VerticalLook.Value, 0f, 0f);
    }
}
