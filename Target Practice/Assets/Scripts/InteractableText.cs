using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableText : MonoBehaviour
{
    public UnityEvent OnHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) return;
        OnHit?.Invoke();
    }
}
