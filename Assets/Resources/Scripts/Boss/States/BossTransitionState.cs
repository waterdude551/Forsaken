using UnityEngine;
public class BossTransitionState : State
{
    private BossStateMachine bossContext;
    public BossTransitionState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
        isBaseState = true;
    }
    public override void EnterState()
    {   
        bossContext.IntroFinished = 0;
        if (bossContext.CurrentStage == 1)
        {
            bossContext.Anim.Play("Phase One");
        } else if (bossContext.CurrentStage == 2)
        {
            bossContext.Anim.Play("Phase Two");
        } else
        {
            bossContext.Anim.Play("Phase Three");
        }
        bossContext.AppliedMovementX = 0f;
        bossContext.AppliedMovementY = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.IntroFinished = 1;
        bossContext.IsTransitioning = false;
        bossContext.AppliedMovementX = 0f;
        bossContext.AppliedMovementY = 0f;
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IntroFinished == 1)
        {
            SwitchState(new BossIdleState(bossContext));
        }
    }
}
