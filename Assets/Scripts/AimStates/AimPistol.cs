using UnityEngine;

public class AimPistol : StateBase<Movement>
{
    public AimPistol(Movement agent) : base(agent)
    {
    }
    public override void Enter()
    {
        Agent.Anim.SetBool("IsAiming", true);
    }
    public override void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Exit();
        }
    }
    public override void Exit()
    {
        Agent.Anim.SetBool("IsAiming", false);
        Agent.Anim.SetLayerWeight(1, 0);
    }
}