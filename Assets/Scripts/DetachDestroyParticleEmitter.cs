using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachDestroyParticleEmitter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles = null;
    [SerializeField]
    private float TimeToDestroy = 2f;

    // Start is called before the first frame update
    void Start()
    {
        particles.transform.SetParent(null);
        Destroy(particles, TimeToDestroy);
    }

    void OnDestroy()
    {
        if (particles)
        {
            particles.Stop();
        }
    }
}
