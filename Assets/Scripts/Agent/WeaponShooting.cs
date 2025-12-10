using System;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _shootingRate;
    [SerializeField] private float _shootingPushForce;
    [SerializeField] private float _shootingDamage;
    private GameInput _input;
    private float _fireInput;
    void Awake()
    {
        _input = new GameInput();
        _input.Player.Fire.performed += ctx => _fireInput = ctx.ReadValue<float>();
        _input.Player.Fire.canceled += ctx => _fireInput = 0f;
        _input.Enable();
    }
    void Update()
    {
        if (_fireInput != 0f)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(_muzzleTransform.position, _muzzleTransform.forward, out RaycastHit hit, _shootingRange))
        {
            if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.OnDamage(_shootingDamage);
            }
        }
    }
}
