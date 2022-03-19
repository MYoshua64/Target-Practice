using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent OnHit;

    float maxPoints = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) return;
        Vector3 hitOffset = collision.contacts[0].point - transform.position;
        Vector3 hitOffsetLocal = transform.InverseTransformDirection(hitOffset);
        if (hitOffsetLocal.x >= 0) return;
        float distanceFromCenter = hitOffset.magnitude * 20f;
        int pointsToGive = Mathf.RoundToInt(maxPoints - Mathf.RoundToInt(distanceFromCenter)) + 1;

        EventManager.instance.OnTargetHit?.Invoke(pointsToGive);
        OnHit?.Invoke();
    }
}
