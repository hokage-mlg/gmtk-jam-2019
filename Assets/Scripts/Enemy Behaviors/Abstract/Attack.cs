using UnityEngine;

public abstract class Attack : EnemyBehavior
{
    [SerializeField]
    protected float cooldown = 1f;

    protected override void Awake()
    {
        base.Awake();
        cooldownLeft = cooldown;
    }

    public override void CalledUpdate()
    {
        base.CalledUpdate();
        cooldownLeft = Mathf.Max(cooldownLeft - Time.deltaTime, 0);
        if (cooldownLeft <= 0)
        {
            DoAttack();
            cooldownLeft = cooldown;
        }
    }

    protected abstract void DoAttack();

    protected float cooldownLeft;
}