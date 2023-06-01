using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler0601 : MonoBehaviour
{
    [SerializeField] Poolable0601 pooledObject;

    [SerializeField] int poolSize;
    [SerializeField] int maxSize;

    private Stack<Poolable0601> objectPooler = new Stack<Poolable0601>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Poolable0601 pooling = Instantiate(pooledObject);
            pooling.gameObject.SetActive(false);
            pooling.transform.SetParent(transform);
            pooling.Pool = this;
            objectPooler.Push(pooling);
        }
    }

    public Poolable0601 Get()
    {
        if (objectPooler.Count > 0)
        {
            Poolable0601 pooling = objectPooler.Pop();
            pooling.gameObject.SetActive(true);
            pooling.transform.parent = null;
            return pooling;
        }
        else
        {
            Poolable0601 pooling = Instantiate(pooledObject);
            pooling.Pool = this;
            return pooling;
        }
    }

    public void Release(Poolable0601 pooling)
    {
        if (objectPooler.Count < maxSize)
        {
            pooling.gameObject.SetActive(false);
            pooling.transform.SetParent(transform);
            objectPooler.Push(pooling);
        }
        else
        {
            Destroy(pooling.gameObject);
        }
    }
}
