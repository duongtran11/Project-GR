using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform _weaponNormalPosition;
    [SerializeField] private Transform _weaponADSPosition;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private float _aimSpeed;

    void Start()
    {
        _currentWeapon.transform.position = _weaponNormalPosition.localPosition;
    }

    public void SwitchAimingMode(bool isAiming)
    {
        if (isAiming)
        {
            _currentWeapon.StartPosition = _weaponADSPosition.localPosition;
            _currentWeapon.transform.localPosition = Vector3.Lerp(_currentWeapon.transform.localPosition, _weaponADSPosition.localPosition, _aimSpeed * Time.deltaTime);
        }
        else
        {
            _currentWeapon.StartPosition = _weaponNormalPosition.localPosition;
            _currentWeapon.transform.localPosition = Vector3.Lerp(_currentWeapon.transform.localPosition, _weaponNormalPosition.localPosition, _aimSpeed * Time.deltaTime);
        }
    }
}
