using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable0601 : MonoBehaviour
{
    [SerializeField] float releaseTime;

    private ObjectPooler0601 pool;
    public ObjectPooler0601 Pool { get { return pool; } set { pool = value; } }

    IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }

    private void OnEnable()
    {
        StartCoroutine(ReleaseTimer());
    }
}
