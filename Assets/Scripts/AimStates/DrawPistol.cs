using UnityEngine;

public class DrawPistol : WeaponStateBase
{
    public DrawPistol(Weapon agent) : base(agent)
    {
    }
    public override void Enter()
    {
        Agent.Anim.SetBool("IsHandGun", true);
    }
    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var aimState = Agent.StateFactory.GetOrCreate<AimPistol>(Agent);
            Agent.StateMachine.ChangeState(aimState);
        }
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsHandGun", false);
    }
}