using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SimpleAgentAimController : MonoBehaviour, IInputAxisOwner
{
    public InputAxis HorizontalLook = InputAxis.DefaultMomentary;
    public InputAxis VerticalLook = InputAxis.DefaultMomentary;
    private float _horizontal;
    private float _vertical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _horizontal += HorizontalLook.Value;
        _vertical += VerticalLook.Value;

        transform.localRotation = Quaternion.Euler(_vertical, _horizontal, 0f);
    }

    void IInputAxisOwner.GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref HorizontalLook, Name = "HorizontalLook" });
        axes.Add(new() { DrivenAxis = () => ref VerticalLook, Name = "VerticalLook" });
    }
}
