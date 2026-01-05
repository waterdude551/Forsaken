using UnityEngine;
public class BossGrappleState : State
{
    private BossStateMachine bossContext;
    private LineRenderer lineRenderer;
    private float timer;

    public BossGrappleState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }

    public override void EnterState()
    {
        lineRenderer = bossContext.GetComponentInChildren<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on boss GameObject");
            SwitchState(new BossIdleState(bossContext));
            return;
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, bossContext.transform.position);
        lineRenderer.SetPosition(1, bossContext.Player.transform.position);

        timer = 0f;
        bossContext.AppliedMovementX = 0f;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;

        Vector3 direction = (bossContext.Player.transform.position - bossContext.transform.position).normalized;
        bossContext.transform.position += direction * bossContext.GrappleSpeed * Time.deltaTime;

        lineRenderer.SetPosition(0, bossContext.transform.position);
        lineRenderer.SetPosition(1, bossContext.Player.transform.position);

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
        bossContext.IsGrappling = false;

    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IsHurt)
        {
            SwitchState(new BossHurtState(bossContext));
        }
        else if (timer >= bossContext.GrappleDuration)
        {
            SwitchState(new BossIdleState(bossContext));
        }
    }
}