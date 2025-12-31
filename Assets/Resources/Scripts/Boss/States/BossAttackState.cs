using UnityEngine;

public class BossAttackState : State
{
    private BossStateMachine bossContext;
    public BossAttackState(BossStateMachine currentContext) : base(currentContext)
    {
        
        bossContext = currentContext;
    }
    public override void EnterState()
    {
        bossContext.AttackFinished = 0;
        bossContext.Anim.SetBool("isAttacking", true);
        bossContext.AppliedMovementX = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.AttackFinished = 0;
        bossContext.Anim.SetBool("isAttacking", false);
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IsHurt)
        {
            SwitchState(new BossHurtState(bossContext));
        }
        else if (bossContext.AttackFinished == 1)
        {
            SwitchState(new BossIdleState(bossContext));
        }
        
    }
}
