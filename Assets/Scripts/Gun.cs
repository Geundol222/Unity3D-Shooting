using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();
            // ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            ParticleSystem effect = GameManager.Resource.Instantiate<ParticleSystem>("Prefabs/HitEffect", hit.point, Quaternion.LookRotation(hit.normal), true);
            effect.transform.parent = hit.transform;
            GameManager.Resource.Destroy(effect.gameObject, 3f);

            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            hittable?.Hit(hit, damage);
        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
        }
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        // TrailRenderer trail = Instantiate(bulletTrail, startPoint, Quaternion.identity);
        TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("Prefabs/BulletTrail", startPoint, Quaternion.identity, true);
        trail.Clear();

        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }

        // Destroy(trail.gameObject, 3f);
        GameManager.Resource.Destroy(trail.gameObject);

        // yield return null;

        // if (!trail.IsValid())
        // {
        //     Debug.Log("트레일이 없다.");
        // }
        // else
        // {
        //     Debug.Log("트레일이 있다.");
        // }
    }
}
