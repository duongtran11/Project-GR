using UnityEngine;

public class DrawPistol : WeaponStateBase
{
    public DrawPistol(Weapon agent) : base(agent)
    {
    }
    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var aimState = Agent.StateFactory.GetOrCreate<AimPistol>(Agent);
            Agent.StateMachine.ChangeState(aimState);
        }
    }

}