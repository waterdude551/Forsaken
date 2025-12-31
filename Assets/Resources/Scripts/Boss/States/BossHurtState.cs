using UnityEngine;
public class BossHurtState : State
{
    private BossStateMachine bossContext;
    public BossHurtState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }
    public override void EnterState()
    {
        bossContext.HurtFinished = 0;
        bossContext.Anim.SetBool("isHurt", true);
        bossContext.AppliedMovementX = 0f;
        bossContext.AppliedMovementY = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.IsHurt = false;
        bossContext.Anim.SetBool("isHurt", false);
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.HurtFinished == 1)
        {
            SwitchState(new BossIdleState(bossContext));
        }
        
    }
}
