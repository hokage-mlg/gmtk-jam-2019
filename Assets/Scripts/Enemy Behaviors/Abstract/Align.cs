using UnityEngine;

public abstract class Align : EnemyBehavior
{
    public float targetRadius;
    public float slowRadius = 0.1f;
    public float timeToTarget = 0.1f;

    public override EnemySteering GetSteering()
    {
        EnemySteering steering = new EnemySteering();

        float rotation = targetOrientation - agent.orientation;
        rotation = MapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);
        if (rotationSize < targetRadius)
            return steering;
        float targetRotation = agent.maxRotation;
        if (rotationSize < slowRadius)
            targetRotation = agent.maxRotation * rotationSize / slowRadius;
        targetRotation *= Mathf.Sign(rotation);
        steering.angular = targetRotation - agent.rotation;
        steering.angular /= timeToTarget;
        float angularAccel = Mathf.Abs(steering.angular);
        if (angularAccel > agent.maxAngularAccel)
        {
            steering.angular /= angularAccel;
            steering.angular *= agent.maxAngularAccel;
        }

        return steering;
    }

    protected float targetOrientation;
}
