using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseParticleEmission : MonoBehaviour
{
    [SerializeField]
    private bool ActivateOnStart = true;
    private ParticleSystem particles;

    [SerializeField]
    private float EmissionMultiplier = 3;
    [SerializeField]
    private float Duration = 1f;
    private ParticleSystem.EmissionModule em;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        em = particles.emission;
        if (ActivateOnStart)
        {
            IncreaseEmission();
        }
    }

    public void IncreaseEmission()
    {
        em.rateOverTimeMultiplier *= EmissionMultiplier;
        StartCoroutine(StopEffect());
    }

    private IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(Duration);
        em.rateOverTimeMultiplier *= (1.0f / EmissionMultiplier);
    }
}
