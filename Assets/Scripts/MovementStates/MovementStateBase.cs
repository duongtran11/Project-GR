using UnityEngine;

public class MovementStateBase : StateBase<Movement>
{
    public MovementStateBase(Movement agent) : base(agent)
    {
        Agent = agent;
    }
    public override void Enter()
    {
        base.Enter();
        Agent.Anim.SetLayerWeight(0, 1);
    }
    public override void Exit() { }
    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            var runState = Agent.MovementSF.GetOrCreate<RunState>(Agent);
            Agent.MovementSM.ChangeState(runState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.RunSpeed
                                : Agent.BackwardRunSpeed;

            return;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Crouching
            var crouchState = Agent.MovementSF.GetOrCreate<CrouchState>(Agent);
            Agent.MovementSM.ChangeState(crouchState);
            Agent.MoveSpeed = Agent.HorizontalInput >= 0
                                ? Agent.CrouchSpeed
                                : Agent.BackwardCrouchSpeed;
            return;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Agent.IsHandGun = !Agent.IsHandGun;
            if (Agent.IsHandGun)
            {
                var drawState = Agent.MovementSF.GetOrCreate<DrawPistol>(Agent);
                Agent.MovementSM.ChangeState(drawState);
            }
            else
            {
                Agent.Anim.SetLayerWeight(1, 0);
                Agent.Anim.SetBool("IsHandGun", false);
                var idleState = Agent.MovementSF.GetOrCreate<IdleState>(Agent);
                Agent.MovementSM.ChangeState(idleState);
            }

            return;
        }
        if (Agent.MoveDirection.magnitude < 0.1f)
        {
            // idle
            var idleState = Agent.MovementSF.GetOrCreate<IdleState>(Agent);
            Agent.MovementSM.ChangeState(idleState);
            return;
        }
    }
}