using UnityEngine;

public class MoveForward : EnemyBehavior
{
    public override EnemySteering GetSteering()
    {
        EnemySteering steering = new EnemySteering();
        steering.linear = transform.rotation * Vector3.up;
        steering.linear *= agent.maxAccel;

        return steering;
    }
}
