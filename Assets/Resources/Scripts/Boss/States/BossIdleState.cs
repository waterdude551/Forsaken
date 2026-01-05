using UnityEngine;
public class BossIdleState : State
{
    private BossStateMachine bossContext;
    private float curTime;
    public BossIdleState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
        isBaseState = true;
    }
    public override void EnterState()
    {
        bossContext.Anim.Play("Idle");
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
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            bossContext.IsGrappling = true;
            SwitchState(new BossGrappleState(bossContext));
        }
        if (curTime > bossContext.TimeInIdle)
        {
            if (bossContext.CurrentStage == 1)
            {
                SwitchState(new StageOne(bossContext));
            }
            else if (bossContext.CurrentStage == 2)
            {
                SwitchState(new StageTwo(bossContext));
            } else if (bossContext.CurrentStage == 3)
            {
                SwitchState(new StageThree(bossContext));
            }
        } 
    }
}
