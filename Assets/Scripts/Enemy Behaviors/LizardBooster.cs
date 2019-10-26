using UnityEngine;

public class LizardBooster : EnemyBehavior
{
    [SerializeField]
    private float boostedSpeed = 5.0f;

    [SerializeField]
    private Vector2 cooldownTimeRange = new Vector2(4f, 12f);

    [SerializeField]
    private float boostTime = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        baseSpeed = agent.maxSpeed;
        cooldownTimeLeft = Random.Range(cooldownTimeRange.x, cooldownTimeRange.y);
        boostTimeLeft = 0.0f;
    }

    public override void CalledUpdate()
    {
        cooldownTimeLeft = Mathf.Max(cooldownTimeLeft - Time.deltaTime, 0);
        boostTimeLeft = Mathf.Max(boostTimeLeft - Time.deltaTime, 0);

        if (boostTimeLeft <= 0)
        {
            agent.maxSpeed = baseSpeed;
        }

        if (cooldownTimeLeft <= 0)
        {
            var audio = GetComponent<AudioSource>();
            AudioManager.Play("LizardRun", audio);

            cooldownTimeLeft = Random.Range(cooldownTimeRange.x, cooldownTimeRange.y);
            boostTimeLeft = boostTime;
            agent.maxSpeed = boostedSpeed;
        }
    }

    private float baseSpeed;
    private float cooldownTimeLeft;
    private float boostTimeLeft;
}
