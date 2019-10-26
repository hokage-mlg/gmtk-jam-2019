using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    // public float priority = 1.0f;
    public float weight = 1.0f;
    protected GameObject target;
    protected AIAgent agent;

    protected virtual void Awake()
    {
        agent = gameObject.GetComponent<AIAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void CalledUpdate()
    {
        agent.SetSteering(GetSteering(), weight);
    }

    public virtual EnemySteering GetSteering() {
        return new EnemySteering();
    }

    protected float MapToRange(float rotation)
    {
        rotation %= 360.0f;
        if (Mathf.Abs(rotation) > 180.0f)
        {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else
                rotation -= 360.0f;
        }
        return rotation;
    }
}