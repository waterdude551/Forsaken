using UnityEngine;
public class BossIdleState : State
{
    private BossStateMachine bossContext;
    private float curTime;
    public BossIdleState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }
    public override void EnterState()
    {
        bossContext.Anim.SetBool("isIdle", true);
        bossContext.AppliedMovementX = 0f;
        bossContext.AppliedMovementY = 0f;
        curTime = 0f;
    }
    public override void UpdateState()
    {
        curTime += Time.deltaTime;
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.Anim.SetBool("isIdle", false);
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IsHurt)
        {
            SwitchState(new BossHurtState(bossContext));
        }
        else if (curTime > bossContext.TimeInIdle && bossContext.InRange())
        {
            SwitchState(new BossAttackState(bossContext));
        } else if (curTime > bossContext.TimeInIdle)
        {   
            SwitchState(new BossWalkState(bossContext));
        } 
    }
}
