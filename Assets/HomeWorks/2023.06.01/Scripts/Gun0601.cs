using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun0601 : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();
            hittable?.Hit(hit, damage);
        }
    }
}
