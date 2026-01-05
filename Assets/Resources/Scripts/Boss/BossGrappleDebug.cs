using UnityEngine;

public class BossGrappleDebugger : MonoBehaviour
{
    public Transform player;
    public LineRenderer lineRenderer;
    
    public float grappleSpeed = 20f;
    public float stopDistance = 1.5f;

    private bool isGrappling = false;

    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            isGrappling = true;
            if(lineRenderer != null) lineRenderer.enabled = true;
            Debug.Log("DEBUG: Grapple Started");
        }

        if (isGrappling)
        {
            PerformGrapple();
        }
    }

   void PerformGrapple()
    {
        if (player == null) return;

        // calc midpts of colliders
        Vector3 bossCenter = GetComponent<Collider2D>().bounds.center;
        Vector3 playerCenter = player.GetComponent<Collider2D>().bounds.center;

        transform.position = Vector3.MoveTowards(transform.position, playerCenter, grappleSpeed * Time.deltaTime);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, bossCenter); 
            lineRenderer.SetPosition(1, playerCenter);
        }

        if (Vector3.Distance(bossCenter, playerCenter) < stopDistance)
        {
            isGrappling = false;
            if(lineRenderer != null) lineRenderer.enabled = false;
        }
    }
}