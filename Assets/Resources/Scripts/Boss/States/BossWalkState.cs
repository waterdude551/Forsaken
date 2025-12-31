using UnityEngine;
public class BossWalkState : State
{
    private BossStateMachine bossContext;
    public BossWalkState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }
    public override void EnterState()
    {
        bossContext.Anim.SetBool("isWalking", true);
        
    }
    public override void UpdateState()
    {
        Vector3 target = new Vector3(bossContext.Player.gameObject.transform.position.x, bossContext.RB.gameObject.transform.position.y, 0f);
        Vector3 currentPos = new Vector3(bossContext.RB.gameObject.transform.position.x, bossContext.RB.gameObject.transform.position.y, 0f);
        Vector3 direction = (target - currentPos).normalized;
        bossContext.AppliedMovementX = direction.x * bossContext.MoveSpeed;
        
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.Anim.SetBool("isWalking", false);
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IsHurt)
        {
            SwitchState(new BossHurtState(bossContext));
        }
        else if (bossContext.InRange())
        {
            SwitchState(new BossAttackState(bossContext));
        }
    }
}
