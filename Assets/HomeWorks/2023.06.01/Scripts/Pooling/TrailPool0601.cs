using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPool0601 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform muzzleEffect;

    private ObjectPooler0601 objectPool;
    private Gun0601 gun;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPooler0601>();
    }

    public void Fire(RaycastHit hit, float maxDistance)
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            StartCoroutine(TrailRoutine(muzzleEffect.position, hit.point));
        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
        }
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        Poolable0601 poolable = objectPool.Get();

        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            poolable.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }
    }

}
