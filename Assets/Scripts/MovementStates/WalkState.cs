public class WalkState : MovementStateBase
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
        base.Update();
    }
}
