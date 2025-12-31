using UnityEngine;
public class BossTransitionState : State
{
    private BossStateMachine bossContext;
    public BossTransitionState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }
    public override void EnterState()
    {   Debug.Log("transitioning");
        bossContext.IntroFinished = 0;
        if (bossContext.CurrentStage == 1)
        {
            bossContext.Anim.SetTrigger("phaseOne");
        } else if (bossContext.CurrentStage == 2)
        {
            bossContext.Anim.SetTrigger("phaseTwo");
        } else
        {
            bossContext.Anim.SetTrigger("phaseThree");
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
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IntroFinished == 1)
        {
            SwitchState(new BossIdleState(bossContext));
        }
    }
}
