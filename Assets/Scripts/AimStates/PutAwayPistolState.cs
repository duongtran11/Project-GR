public class PutAwayPistolState : WeaponStateBase
{
    public PutAwayPistolState(Weapon agent) : base(agent)
    {
    }
    public override void Exit()
    {
        Agent.Anim.SetLayerWeight(1, 0);
    }

}