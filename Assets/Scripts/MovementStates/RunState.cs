using UnityEngine;

public class RunState : StateBase<Movement>
{
    public RunState(Movement agent) : base(agent)
    {
    }

    public override void Enter()
    {
        Agent.Anim.SetBool("IsRunning", true);
    }

    public override void Exit()
    {
        Agent.Anim.SetBool("IsRunning", false);
    }

    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // walking
            var walkState = Agent.MovementSF.GetOrCreate<WalkState>(Agent);
            Agent.MovementSM.ChangeState(walkState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.WalkSpeed
                                : Agent.BackwardWalkSpeed;
        }
        if (Agent.MoveDirection.magnitude < 0.1f)
        {
            // idle
            var idleState = Agent.MovementSF.GetOrCreate<IdleState>(Agent);
            Agent.MovementSM.ChangeState(idleState);
        }
    }
}
