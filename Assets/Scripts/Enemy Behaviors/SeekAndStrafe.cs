using UnityEngine;

public class SeekAndStrafe : Seek
{
    public override EnemySteering GetSteering()
    {
        EnemySteering steering = base.GetSteering();
        var quat = new Quaternion();
        quat.eulerAngles = new Vector3(0, 0, dirChangeAngle);
        steering.linear = quat * steering.linear;

        return steering;
    }

    public override void CalledUpdate()
    {
        if (dirChangeTimer <= 0)
        {
            dirChangeAngle = Random.Range(-dirChangeMaxAngle, dirChangeMaxAngle);
            dirChangeTimer = dirChangeTimeMax;
        }
        dirChangeTimer -= Time.deltaTime;
        base.CalledUpdate();
    }

    private float dirChangeTimer = 0.0f;
    private float dirChangeTimeMax = 2.0f;
    private float dirChangeAngle = 0.0f;
    private float dirChangeMaxAngle = 50.0f;
}
