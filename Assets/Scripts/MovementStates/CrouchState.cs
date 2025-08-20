using UnityEngine;

public class CrouchState : StateBase<Movement>
{
    public CrouchState(Movement agent) : base(agent)
    {
    }

    public override void Enter()
    {
        Agent.Anim.SetBool("IsCrouching", true);
    }

    public override void Exit()
    {
        Agent.Anim.SetBool("IsCrouching", false);
    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            var runState = Agent.MovementSF.GetOrCreate<RunState>(Agent);
            Agent.MovementSM.ChangeState(runState);
        }
        if (Agent.MoveDirection.magnitude < 0.1f)
        {
            // idle
            var idleState = Agent.MovementSF.GetOrCreate<IdleState>(Agent);
            Agent.MovementSM.ChangeState(idleState);
        }
    }
}
