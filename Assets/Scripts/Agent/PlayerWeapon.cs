using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private StateMachine<PlayerWeapon> _stateMachine = new();
    private StateFactory<PlayerWeapon> _stateFactory = new();
    public StateMachine<PlayerWeapon> StateMachine => _stateMachine;
    public StateFactory<PlayerWeapon> StateFactory => _stateFactory;
    public Animator Anim { get; set; }
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        _stateMachine?.CurrentState?.Update();
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
    }
}