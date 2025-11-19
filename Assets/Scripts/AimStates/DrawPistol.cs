public class DrawPistol : StateBase<Movement>
{
    public DrawPistol(Movement agent) : base(agent)
    {
    }
    public override void Enter()
    {
        Agent.Anim.SetTrigger("DrawPistol");
    }
    public override void Update()
    {
        Agent.Anim.SetLayerWeight(1, 1);
        var drawState = Agent.MovementSF.GetOrCreate<AimPistol>(Agent);
        Agent.MovementSM.ChangeState(drawState);
    }
    public override void Exit()
    {

    }
}