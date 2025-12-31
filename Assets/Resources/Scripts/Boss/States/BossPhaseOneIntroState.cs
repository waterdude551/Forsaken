using UnityEngine;
public class BossPhaseOneIntroState : State
{
    private BossStateMachine bossContext;
    public BossPhaseOneIntroState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }
    public override void EnterState()
    {   
        bossContext.IntroFinished = 0;
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
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IntroFinished == 1)
        {
            SwitchState(new BossIdleState(bossContext));
        }
    }
}
