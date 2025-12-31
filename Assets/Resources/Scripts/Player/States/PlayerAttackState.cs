using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerStateMachine playerContext;
    public PlayerAttackState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
    }
    public override void EnterState()
    {
        playerContext.Anim.SetBool("isAttacking", true);
        playerContext.AppliedMovementX = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        playerContext.Anim.SetBool("isAttacking", false);
    }

    public override void CheckSwitchStates()
    {
        if (playerContext.IsHurt)
        {
            SwitchState(new PlayerHurtState(playerContext));
        }
        else if (!playerContext.AttackFinished)
        {
            return;
        }
        playerContext.AttackFinished = false; 
        if (playerContext.IsMovementPressed && playerContext.IsRunPressed)
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
