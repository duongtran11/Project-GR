using UnityEngine;

public class AimPistol : MovementStateBase
{
    public AimPistol(Movement agent) : base(agent)
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
            var holdState = Agent.MovementSF.GetOrCreate<DrawPistol>(Agent);
            Agent.MovementSM.ChangeState(holdState);
        }
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsAiming", false);
    }
}