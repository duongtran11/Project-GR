using Unity.Cinemachine;
using UnityEngine;

public class AimCameraRig : CinemachineCameraManagerBase
{
    private CinemachineVirtualCameraBase FreeCamera;
    private CinemachineVirtualCameraBase AimCamera;

    protected override CinemachineVirtualCameraBase ChooseCurrentCamera(Vector3 worldUp, float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        foreach (var cam in ChildCameras)
        {
            if (AimCamera == null & cam.TryGetComponent<CinemachineThirdPersonAim>(out var aim)
                && aim.NoiseCancellation)
            {
                AimCamera = cam;
            }
            else if (FreeCamera == null)
            {
                FreeCamera = cam;
            }
        }
    }
}
