public class CrouchState : MovementStateBase
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
        base.Update();
    }
}
