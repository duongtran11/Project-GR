public class WeaponStateBase : StateBase<Weapon>
{
    public WeaponStateBase(Weapon agent) : base(agent)
    {
        Agent = agent;
    }
    public override void Update()
    {
        base.Update();
    }
}