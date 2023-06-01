using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target0601 : MonoBehaviour, IHittable
{
    [SerializeField] ParticleSystem hitEffect;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Hit(RaycastHit hit, int damage)
    {
        ParticleSystem playEffect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        playEffect.transform.parent = hit.transform;

        if (rb != null)
        {
            rb.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);
        }
    }
}
