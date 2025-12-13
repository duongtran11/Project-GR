using UnityEngine;

public class HoldPistolState : WeaponStateBase
{
    public HoldPistolState(PlayerWeapon agent) : base(agent)
    {
    }
    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var aimState = Agent.StateFactory.GetOrCreate<AimPistolState>(Agent);
            Agent.StateMachine.ChangeState(aimState);
        }
    }

}