using UnityEngine;

public class FaceWithOffset : Align
{
    [SerializeField]
    private float offsetAngleMax = 40.0f;

    [SerializeField]
    private Vector2 cooldownRange = new Vector2(2f, 3f);

    protected override void Awake()
    {
        base.Awake();
        offsetAngle = Random.Range(-offsetAngleMax, offsetAngleMax);
        cooldownLeft = Random.Range(cooldownRange.x, cooldownRange.y);
    }

    public override void CalledUpdate()
    {
        cooldownLeft = Mathf.Max(cooldownLeft - Time.deltaTime, 0);
        if (cooldownLeft <= 0)
        {
            offsetAngle = Random.Range(-offsetAngleMax, offsetAngleMax);
            cooldownLeft = Random.Range(cooldownRange.x, cooldownRange.y);
        }
        base.CalledUpdate();
    }

    public override EnemySteering GetSteering()
    {
        Vector2 direction = target.transform.position - transform.position;
        if (direction.magnitude > 0.0f)
        {
            float targetOrientation = Mathf.Atan2(direction.x, direction.y);
            targetOrientation *= Mathf.Rad2Deg;
            targetOrientation += offsetAngle;
            targetOrientation %= 360.0f;
            if (targetOrientation < 0.0f)
            {
                targetOrientation += 360.0f;
            }
            base.targetOrientation = targetOrientation;
        }
        
        return base.GetSteering();
    }

    private float offsetAngle;
    private float cooldownLeft;
}
