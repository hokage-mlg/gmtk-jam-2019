using UnityEngine;

public class GhostPhase : EnemyBehavior
{
    [SerializeField]
    private float CoolDown = 18f;
    [SerializeField]
    private float GhostBoostSpeed = 7f;
    [SerializeField]
    private bool PacifistInBoost = true;
    

    protected override void Awake()
    {
        base.Awake();
        standardSpeed = agent.maxSpeed;
        BoxCollider = GetComponent<BoxCollider2D>();
        CoolDownBefore = CoolDown / 2;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void CalledUpdate()
    {
        base.CalledUpdate();
        CoolDownBefore = Mathf.Max(CoolDownBefore - Time.deltaTime, 0);
        if (CoolDownBefore < 3f)
        {
            BoxCollider.isTrigger = false;
            agent.maxSpeed = standardSpeed;
            var s = sprite.color;
            s.a = 1f;
            sprite.color = s;
        }

        if (CoolDownBefore == 0)
        {
            var audio = GetComponent<AudioSource>();
            AudioManager.Play("Ghost", audio);

            BoxCollider.isTrigger = PacifistInBoost;
            agent.maxSpeed = GhostBoostSpeed;
            var s = sprite.color;
            s.a = 0.5f;
            sprite.color = s;

            CoolDownBefore = CoolDown;
        }
    }

    private float CoolDownBefore;
    private BoxCollider2D BoxCollider;
    private SpriteRenderer sprite;
    private float standardSpeed;
}
