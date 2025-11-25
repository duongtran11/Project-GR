public class IdleState : MovementStateBase
{
    public IdleState(Movement agent) : base(agent)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Agent.MoveDirection.magnitude > 0.1f)
        {
            // Start walking
            var walkState = Agent.StateFactory.GetOrCreate<WalkState>(Agent);
            Agent.StateMachine.ChangeState(walkState);
        }
    }
}
