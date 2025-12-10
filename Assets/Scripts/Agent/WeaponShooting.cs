using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private ParticleSystem _muzzleFlash;

    [Header("Shooting settings")]
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _shootingRate;
    [SerializeField] private float _shootingPushForce;
    [SerializeField] private float _shootingDamage;

    [Header("Recoil settings")]
    [SerializeField] private float _recoilX;
    [SerializeField] private float _recoilY;
    [SerializeField] private float _recoilZ;
    [SerializeField] private float _snappiness;
    private Vector3 _targetRotation;
    private Vector3 _currentRotation;
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
            MuzzleFlash();
            Shoot();
        }
        Recoil();
    }

    private void Recoil()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _snappiness * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    private void MuzzleFlash()
    {
        _muzzleFlash.Play();
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
        _targetRotation += new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
    }
}
