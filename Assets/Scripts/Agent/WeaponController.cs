using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private RigBuilder _rigBuilder;
    [SerializeField] private Rig _weaponRig;
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Transform _rightHandBone;
    [SerializeField] private TwoBoneIKConstraint _leftHandIK;
    public Weapon _currentWeapon;
    private StateMachine<WeaponController> _stateMachine = new();
    private StateFactory<WeaponController> _stateFactory = new();
    public StateMachine<WeaponController> StateMachine => _stateMachine;
    public StateFactory<WeaponController> StateFactory => _stateFactory;
    public Animator Anim { get; set; }
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        _stateMachine?.CurrentState?.Update();
    }
    public void DrawRifle()
    {
        Anim.SetLayerWeight(1, 1);
        _aimRig.weight = 0f;
        EquipWeaponRigAndIK();
        var drawState = _stateFactory.GetOrCreate<HoldRifleState>(this);
        _stateMachine.ChangeState(drawState);
    }
    public void AimRifle()
    {
        _aimRig.weight = 1f;
        var aimState = _stateFactory.GetOrCreate<AimRifleState>(this);
        _stateMachine.ChangeState(aimState);
    }
    public void PutAwayRifle()
    {
        Anim.SetBool("IsRifle", false);
        UnequipWeaponRigAndIK();
    }
    public void DrawHandGun()
    {
        Anim.SetLayerWeight(1, 1);
        var drawState = _stateFactory.GetOrCreate<HoldPistolState>(this);
        _stateMachine.ChangeState(drawState);
    }
    public void AimHandGun()
    {
        var aimState = _stateFactory.GetOrCreate<AimPistolState>(this);
        _stateMachine.ChangeState(aimState);
    }
    public void PutAwayHandGun()
    {
        Anim.SetBool("IsHandGun", false);
    }
    public void ExitWeaponState()
    {
        Anim.SetLayerWeight(1, 0);
        UnequipWeaponRigAndIK();
    }
    private void EquipWeaponRigAndIK()
    {
        _currentWeapon.transform.SetParent(_rightHandBone);
        _currentWeapon.transform.localPosition = _currentWeapon.RightHandGrip.localPosition;
        _currentWeapon.transform.localRotation = _currentWeapon.RightHandGrip.localRotation;

        _leftHandIK.data.target = _currentWeapon.LeftHandIKTarget;
        _leftHandIK.data.hint = _currentWeapon.LeftHandIKHint;

        _rigBuilder.Build();

        _weaponRig.weight = 1f;
    }
    private void UnequipWeaponRigAndIK()
    {
        _weaponRig.weight = 0f;
        _aimRig.weight = 0f;
    }
}