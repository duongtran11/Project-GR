public class RunState : MovementStateBase
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
        base.Update();
    }
}
