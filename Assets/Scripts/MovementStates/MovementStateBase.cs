using UnityEngine;

public class MovementStateBase : StateBase<Movement>
{
    public MovementStateBase(Movement agent) : base(agent)
    {
        Agent = agent;
    }
    public override void Enter()
    {
        base.Enter();
        Agent.Anim.SetLayerWeight(0, 1);
    }
    public override void Exit() { }
    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            var runState = Agent.StateFactory.GetOrCreate<RunState>(Agent);
            Agent.StateMachine.ChangeState(runState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.RunSpeed
                                : Agent.BackwardRunSpeed;

            return;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Crouching
            var crouchState = Agent.StateFactory.GetOrCreate<CrouchState>(Agent);
            Agent.StateMachine.ChangeState(crouchState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.CrouchSpeed
                                : Agent.BackwardCrouchSpeed;
            return;
        }
        if (Agent.MoveDirection.magnitude < 0.1f)
        {
            // idle
            var idleState = Agent.StateFactory.GetOrCreate<IdleState>(Agent);
            Agent.StateMachine.ChangeState(idleState);
            return;
        }
    }
}