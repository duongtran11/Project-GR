using UnityEngine;

public class AimPistol : WeaponStateBase
{
    public AimPistol(Weapon agent) : base(agent)
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
            var holdState = Agent.StateFactory.GetOrCreate<DrawPistol>(Agent);
            Agent.StateMachine.ChangeState(holdState);
        }
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsAiming", false);
    }
}