using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer bulletHole;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 position = collision.contacts[0].point + collision.contacts[0].normal * .01f;
        SpriteRenderer hole = Instantiate(bulletHole, position, Quaternion.LookRotation(collision.contacts[0].normal));
        hole.transform.SetParent(collision.transform);
        Destroy(gameObject);
    }
}
