using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace HomeWork0602
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] ParticleSystem muzzleEffect;
        [SerializeField] float bulletSpeed;
        [SerializeField] float maxDistance;
        [SerializeField] int damage;


        public void Fire()
        {
            RaycastHit hit;

            muzzleEffect.Play();

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                IHittable hittable = hit.transform.GetComponent<IHittable>();

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
            GameManager.Resource.Destroy(trail.gameObject);
        }
    }
}
   
