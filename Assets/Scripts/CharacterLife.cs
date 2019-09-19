using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    public static bool isDeath = false;

    public void Death()
    {
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        CharacterShooting shooting = GetComponent<CharacterShooting>();
        CharacterMovement movement = GetComponent<CharacterMovement>();
        if (movement)
            movement.enabled = false;
        if (collider2D)
            collider2D.isTrigger = true;
        if (shooting)
            shooting.enabled = false;
        isDeath = true;
    }

    public void Alive()
    {
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        CharacterShooting shooting = GetComponent<CharacterShooting>();
        CharacterMovement movement = GetComponent<CharacterMovement>();
        if (movement)
            movement.enabled = false;
        if (collider2D)
            collider2D.isTrigger = true;
        if (shooting)
            shooting.enabled = false;
        isDeath = false;
    }

}
