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
            Agent.AimRifle();
        }
    }

}