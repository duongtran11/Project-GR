using UnityEngine;

public class AimRifleState : WeaponStateBase
{
    public AimRifleState(WeaponController agent) : base(agent)
    {
    }
    public override void Enter()
    {
        Agent.Anim.SetBool("IsAiming", true);
    }
    public override void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            var holdState = Agent.StateFactory.GetOrCreate<HoldRifleState>(Agent);
            Agent.StateMachine.ChangeState(holdState);
        }
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsAiming", false);
    }
}