using UnityEngine;

public class WalkState : StateBase<Movement>
{
    public WalkState(Movement agent) : base(agent)
    {
    }

    public override void Enter()
    {
        Agent.Anim.SetBool("IsWalking", true);
    }

    public override void Exit()
    {
        Agent.Anim.SetBool("IsWalking", false);
    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            var runState = Agent.MovementSF.GetOrCreate<RunState>(Agent);
            Agent.MovementSM.ChangeState(runState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.RunSpeed
                                : Agent.BackwardRunSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // Crouching
            var crouchState = Agent.MovementSF.GetOrCreate<CrouchState>(Agent);
            Agent.MovementSM.ChangeState(crouchState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.CrouchSpeed
                                : Agent.BackwardCrouchSpeed;
        }
        if (Agent.MoveDirection.magnitude < 0.1f)
        {
            // idle
            var idleState = Agent.MovementSF.GetOrCreate<IdleState>(Agent);
            Agent.MovementSM.ChangeState(idleState);
        }
        if (Input.GetMouseButtonDown(1))
        {
            var drawState = Agent.MovementSF.GetOrCreate<DrawPistol>(Agent);
            Agent.MovementSM.ChangeState(drawState);
        }
    }
}
