using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork0602
{
    public class Target : MonoBehaviour, IHittable
    {
        [SerializeField] ParticleSystem hitEffect;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Hit(RaycastHit hit, int damage)
        {
            ParticleSystem effect = GameManager.Resource.Instantiate<ParticleSystem>(hitEffect, hit.point, Quaternion.LookRotation(hit.normal), true);
            effect.transform.parent = hit.transform;
            StartCoroutine(ReleaseRoutine(effect));

            if (rb != null)
            {
                rb.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);
            }
        }

        IEnumerator ReleaseRoutine(ParticleSystem effect)
        {
            yield return new WaitForSeconds(3f);
            GameManager.Resource.Destroy(effect.gameObject);
        }
    }
}