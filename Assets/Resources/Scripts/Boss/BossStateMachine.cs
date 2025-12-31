using UnityEngine;
public class BossStateMachine : StateMachine, IDamageable
{
    [SerializeField] private  float targetDistance;
    [SerializeField] private  float timeInIdle;
    [SerializeField] private  int numStages;
    [SerializeField] private  int damage;
    [SerializeField] private float damageCooldown;
    private int currentStage = 1;
    private bool isFlipped = false;
    private bool isHurt = false; 
    
    private int attackFinished = 0;
    private int hurtFinished = 0;
    private int introFinished = 0;
    private int health;
    
    private float canTakeDamage;

    public bool IsHurt{get {return isHurt;} set {isHurt = value;}}
    public int AttackFinished {get {return attackFinished; } set {attackFinished = value;}}
    public int HurtFinished {get {return hurtFinished; } set {hurtFinished = value;}}
    public int IntroFinished {get {return introFinished; } set {introFinished = value;}}
    public int Health {get {return health;} set {health = value;}}
    public int Damage {get {return damage;}}
    public float Cooldown {get {return damageCooldown;} set {damageCooldown = value;}}
    public float TimeInIdle {get {return timeInIdle;}}
    public float TargetDistance {get {return targetDistance;}}
    public int CurrentStage {get {return currentStage;} set {currentStage = value;}}

    protected override void Init()
    {
        base.Init();
        sprite = transform.Find("Sprite");
        Health = 100;
        canTakeDamage = 0f; 
    }

    protected override void EnterBeginningState()
    {
        currentState = new BossPhaseOneIntroState(this);
        currentState.EnterState();
    }
    protected override void FaceMovement()
    {
        Vector3 flipped = sprite.localScale;
        flipped.x *= -1f;
        if (sprite.position.x < player.transform.position.x && isFlipped)
        {
            sprite.localScale = flipped;
            isFlipped = false;
        } else if (sprite.position.x > player.transform.position.x && !isFlipped)
        {
            sprite.localScale = flipped;
            isFlipped = true;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform == player)
        {
            player.gameObject.GetComponent<PlayerStateMachine>().ApplyDamage(Damage);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (Time.time > canTakeDamage && IntroFinished == 1)
        {
            canTakeDamage = Time.time + Cooldown;
            Health -= damage;
            Debug.Log("Enemy Health: " + Health);
            IsHurt = true;
        }
        if (Health <= 0f)
        {
            CheckWinStatus();
        }
    }

    void CheckWinStatus()
    {
        if (currentStage == numStages)
        {
            Debug.Log("You Win!");
            Time.timeScale = 0f;
        }
        else
        {
            currentStage += 1;
            Debug.Log("entering next stage");
            //insert some way to transition here
        }
    }

    public bool InRange()
    {
        return Vector3.Distance(transform.position,Player.transform.position) <= TargetDistance;
    }


}
