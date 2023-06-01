using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun0601 : MonoBehaviour
{
    [SerializeField] TrailPool0601 pool;
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;

    RaycastHit hit;
    public float MaxDistance { get { return maxDistance; } }
    public RaycastHit Hit { get { return hit; } }

    public void Fire()
    {
        muzzleEffect.Play();

        pool.Fire();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();

            hittable?.Hit(hit, damage);
        }
    }
}
