using UnityEngine;

public class EnemySteering
{
    public float angular;
    public Vector2 linear;
    public EnemySteering()
    {
        angular = 0.0f;
        linear = new Vector2();
    }
}