using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll != null)
        {
            Tutorial.Tutorial1Victory = true;
        }
    }
}