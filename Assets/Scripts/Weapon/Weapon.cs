using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponNormalPosition;
    [SerializeField] private Transform _weaponADSPosition;
    public Vector3 StartPosition;
    public Vector3 TargetRotation;
    public Vector3 CurrentRotation;
    public Vector3 TargetPosition;
    public Vector3 CurrentPosition;
}