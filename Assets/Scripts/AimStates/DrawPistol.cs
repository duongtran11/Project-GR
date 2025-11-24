using UnityEngine;

public class DrawPistol : MovementStateBase
{
    public DrawPistol(Movement agent) : base(agent)
    {
    }
    public override void Enter()
    {
        Agent.Anim.SetLayerWeight(1, 1);
        Agent.Anim.SetBool("IsHandGun", true);
    }
    public override void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var aimState = Agent.MovementSF.GetOrCreate<AimPistol>(Agent);
            Agent.MovementSM.ChangeState(aimState);
        }
        base.Update();
    }
}