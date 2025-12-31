using UnityEngine;

public class PlayerJumpState : State
{
    private PlayerStateMachine playerContext;
    public PlayerJumpState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
    }
    public override void EnterState()
    {
        playerContext.Anim.SetBool("isJumping", true);
        playerContext.Grounded = false;
        playerContext.AppliedMovementX = playerContext.CurrentMovementInput.x * playerContext.MoveSpeed / 3f;
    }
    public override void UpdateState()
    {
        playerContext.AppliedMovementX = playerContext.CurrentMovementInput.x * playerContext.MoveSpeed;
        playerContext.AppliedMovementY = 0f ;
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        playerContext.Anim.SetBool("isJumping", false);
        playerContext.Grounded = true;
    }

    public override void CheckSwitchStates()
    {
        if (playerContext.IsHurt)
        {
            SwitchState(new PlayerHurtState(playerContext));
        } else if (playerContext.Grounded && !playerContext.IsMovementPressed )
        {
            SwitchState(new PlayerIdleState(playerContext));
        } else if (playerContext.Grounded && !playerContext.IsRunPressed)
        {
            SwitchState(new PlayerWalkState(playerContext));
        } else if (playerContext.Grounded && playerContext.IsRunPressed)
        {
            SwitchState(new PlayerRunState(playerContext));
        }
    }
}
