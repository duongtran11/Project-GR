public class IdleState : MovementStateBase
{
    public IdleState(Movement agent) : base(agent)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();
        if (Agent.MoveDirection.magnitude > 0.1f)
        {
            // Start walking
            var walkState = Agent.MovementSF.GetOrCreate<WalkState>(Agent);
            Agent.MovementSM.ChangeState(walkState);
        }
    }
}
