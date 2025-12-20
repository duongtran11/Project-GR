using UnityEngine;

public class WeaponStateBase : StateBase<WeaponController>
{
    public WeaponStateBase(WeaponController agent) : base(agent)
    {
        Agent = agent;
    }
    public override void Enter()
    {
        Agent.Anim.SetBool("IsRifle", true);
    }
    public override void Exit()
    {
        // Agent.Anim.SetBool("IsRifle", false);
    }
    public override void Update()
    {
        base.Update();
    }
}