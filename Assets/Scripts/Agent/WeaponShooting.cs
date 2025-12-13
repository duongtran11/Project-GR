using UnityEngine;

public enum FireMode
{
    SemiAuto,
    FullAuto
}
public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform _muzzleTransform;

    [Header("Shooting settings")]
    [SerializeField] private FireMode _fireMode;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _shootingRate;
    [SerializeField] private float _shootingPushForce;
    [SerializeField] private float _shootingDamage;

    [Header("Recoil settings")]
    [SerializeField] private float _recoilX;
    [SerializeField] private float _recoilY;
    [SerializeField] private float _recoilZ;
    [SerializeField] private float _pushBack;
    [SerializeField] private float _snappiness;
    private Vector3 _startPosition;
    private Vector3 _targetRotation;
    private Vector3 _currentRotation;
    private Vector3 _targetPosition;
    private Vector3 _currentPosition;
    private GameInput _input;
    private bool _isFire;
    private bool _fireOnce;
    private float _lastFireTime;
    void Awake()
    {
        _input = new GameInput();
        _input.Player.Fire.performed += ctx =>
        {
            _isFire = true;
            _fireOnce = true;
        };
        _input.Player.Fire.canceled += ctx =>
        {
            _isFire = false;
        };
        _input.Enable();
        _startPosition = transform.localPosition;
    }
    void Update()
    {
        if (_fireMode == FireMode.SemiAuto)
        {
            if (_fireOnce)
            {
                _fireOnce = false;
                Shoot();
            }
        }
        else if (_fireMode == FireMode.FullAuto)
        {
            if (_isFire && Time.time > _lastFireTime + 1f / _shootingRate)
            {
                Shoot();
                _lastFireTime = Time.time;
            }
        }
        Recoil();
    }

    private void Recoil()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _snappiness * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);

        _targetPosition = Vector3.Lerp(_targetPosition, _startPosition, _snappiness * Time.deltaTime);
        _currentPosition = Vector3.Lerp(_currentPosition, _targetPosition, _snappiness * Time.deltaTime);
        transform.localPosition = _currentPosition;
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
        _targetPosition += new Vector3(0f, 0f, _pushBack);
    }
}
