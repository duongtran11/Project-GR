public class PutAwayPistolState : WeaponStateBase
{
    public PutAwayPistolState(PlayerWeapon agent) : base(agent)
    {
    }
    public override void Exit()
    {
        Agent.Anim.SetLayerWeight(1, 0);
    }

}