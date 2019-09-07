using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPuddle : AOEPuddle
{
    [SerializeField]
    private float slowFactor = 0.5f;

    protected override void ApplyEffect(GameObject objectEntered)
    {
        if (objectEntered.tag == "Player")
        {
            objectEntered.GetComponent<CharacterMovement>().speed *= slowFactor;
        }
    }

    protected override void RemoveEffect(GameObject objectEntered)
    {
        if (objectEntered.tag == "Player")
        {
            objectEntered.GetComponent<CharacterMovement>().speed *= 1 / slowFactor;
        }
    }
}
