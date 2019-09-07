using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEPuddle : MonoBehaviour
{
    [SerializeField]
    protected float lifeSpan = 10;
    protected List<GameObject> enteredList; 

    protected virtual void Start()
    {
        enteredList = new List<GameObject>();
        Destroy(gameObject, lifeSpan);
    }

    protected virtual void ApplyEffect(GameObject objectEntered) { }

    protected void OnDestroy()
    {
        ForceRemoveEffect();
    }

    protected void ForceRemoveEffect()
    {
        foreach (var obj in enteredList)
        {
            if (obj != null) RemoveEffect(obj);
        }
    }

    protected virtual void RemoveEffect(GameObject objectExited) { }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (enteredList.Contains(coll.gameObject)) return;

        enteredList.Add(coll.gameObject);
        ApplyEffect(coll.gameObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        if (!enteredList.Contains(coll.gameObject)) return;

        enteredList.Remove(coll.gameObject);
        RemoveEffect(coll.gameObject);
    }
}
