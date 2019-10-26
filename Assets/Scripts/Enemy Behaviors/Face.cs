using UnityEngine;

public class Face : Align
{
    public override EnemySteering GetSteering()
    {
        Vector2 direction = target.transform.position - transform.position;
        if (direction.magnitude > 0.0f)
        {
            float targetOrientation = Mathf.Atan2(direction.x, direction.y);
            targetOrientation *= Mathf.Rad2Deg;
            base.targetOrientation = targetOrientation;
        }
        
        return base.GetSteering();
    }
}
