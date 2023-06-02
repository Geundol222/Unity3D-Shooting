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


    public void Fire()
    {
        RaycastHit hit;

        muzzleEffect.Play();
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();

            pool.Fire(hit, maxDistance);

            hittable?.Hit(hit, damage);
        }
        else
        {
            pool.Fire(hit, maxDistance);
        }
    }
}
