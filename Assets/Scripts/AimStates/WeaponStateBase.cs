using UnityEngine;

public class WeaponStateBase : StateBase<PlayerWeapon>
{
    public WeaponStateBase(PlayerWeapon agent) : base(agent)
    {
        Agent = agent;
    }
    public override void Enter()
    {
        Agent.Anim.SetBool("IsHandGun", true);
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsHandGun", false);
    }
    public override void Update()
    {
        base.Update();
    }
}