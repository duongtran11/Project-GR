using UnityEngine;

public class HoldRifleState : WeaponStateBase
{
    public HoldRifleState(WeaponController agent) : base(agent)
    {
    }
    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var aimState = Agent.StateFactory.GetOrCreate<AimRifleState>(Agent);
            Agent.StateMachine.ChangeState(aimState);
        }
    }

}