using UnityEngine;

public class Weapon : MonoBehaviour
{
    private StateMachine<Weapon> _stateMachine = new();
    private StateFactory<Weapon> _stateFactory = new();
    public StateMachine<Weapon> StateMachine => _stateMachine;
    public StateFactory<Weapon> StateFactory => _stateFactory;
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
        var drawState = _stateFactory.GetOrCreate<DrawPistol>(this);
        _stateMachine.ChangeState(drawState);
    }
    public void AimHandGun()
    {
        var aimState = _stateFactory.GetOrCreate<AimPistol>(this);
        _stateMachine.ChangeState(aimState);
    }
    public void PutAwayHandGun()
    {
        Anim.SetLayerWeight(1, 0);
        Anim.SetBool("IsHandGun", false);
    }
}