using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TPSCameraRig : CinemachineCameraManagerBase, IInputAxisOwner
{
    public InputAxis Aim = InputAxis.DefaultMomentary;
    private TPSAgentAimController _aimController;
    private CinemachineVirtualCameraBase _freeCamera;
    private CinemachineVirtualCameraBase _aimCamera;
    private bool IsAiming => Aim.Value > 0.5f;

    public void GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref Aim, Name = "Aim" });
    }

    protected override CinemachineVirtualCameraBase ChooseCurrentCamera(Vector3 worldUp, float deltaTime)
    {
        var oldCam = (CinemachineVirtualCameraBase)LiveChild;
        var newCam = IsAiming ? _aimCamera : _freeCamera;
        if (newCam != oldCam && _aimController != null)
        {
            _aimController.CameraCouplingMode = IsAiming
                ? TPSAgentAimController.CouplingMode.Coupled
                : TPSAgentAimController.CouplingMode.Decoupled;
            _aimController.Recenter();
        }
        return newCam;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        foreach (var cam in ChildCameras)
        {
            if (_aimCamera == null & cam.TryGetComponent<CinemachineThirdPersonAim>(out var aim)
                && aim.NoiseCancellation)
            {
                _aimCamera = cam;
                var player = _aimCamera.Follow;
                if (player != null)
                {
                    _aimController = player.GetComponentInChildren<TPSAgentAimController>();
                }
            }
            else if (_freeCamera == null)
            {
                _freeCamera = cam;
            }
        }
    }
}
