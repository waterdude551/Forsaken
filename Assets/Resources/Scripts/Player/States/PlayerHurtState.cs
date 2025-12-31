using UnityEngine;
public class PlayerHurtState : State
{
    private PlayerStateMachine playerContext;
    public PlayerHurtState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
    }
    public override void EnterState()
    {
        playerContext.Anim.SetBool("isHurt", true);
        playerContext.AppliedMovementX = 0f;
        playerContext.AppliedMovementY = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        playerContext.IsHurt = false;
        playerContext.Anim.SetBool("isHurt", false);
    }

    public override void CheckSwitchStates()
    {
        if (!playerContext.HurtFinished)
        {
            return;
        }
        playerContext.HurtFinished = false;
        if (playerContext.IsHitPressed)
        {
            SwitchState(new PlayerAttackState(playerContext));
        }
        else if (playerContext.IsMovementPressed && playerContext.IsRunPressed)
        {
            SwitchState(new PlayerRunState(playerContext));
        } else if (playerContext.IsMovementPressed)
        {   
            SwitchState(new PlayerWalkState(playerContext));
        } else
        {
            SwitchState(new PlayerIdleState(playerContext));
        }
    }
}
