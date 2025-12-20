using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Transform WeaponNormalPosition;
    public Transform WeaponADSPosition;
    public Transform RightHandGrip;
    public Transform LeftHandIKTarget;
    public Transform LeftHandIKHint;
    public Vector3 StartPosition;
    public Vector3 TargetRotation;
    public Vector3 CurrentRotation;
    public Vector3 TargetPosition;
    public Vector3 CurrentPosition;
}