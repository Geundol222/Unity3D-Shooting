using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, ObjectPool<GameObject>> poolDic;

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();
    }

    //public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    //{
    //    if (!poolDic.ContainsKey(prefab.name))
    //        CreatPool(prefab.name, prefab);

    //    ObjectPool<GameObject> pool = poolDic[prefab.name];
    //    GameObject obj = pool.Get();
    //    obj.transform.position = position;
    //    obj.transform.rotation = rotation;
    //    return obj;
    //}


    // 오브젝트 풀링의 일반화
    // 이렇게 일반화 하는 것은 박싱 언박싱이 너무 빈번하게 일어날 수 있으므로 완전히 올바른 방식은 아니다.
    // 이해를 돕기 위한것
    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;

            if (!poolDic.ContainsKey(prefab.name))
                CreatePool(prefab.name, prefab);

            ObjectPool<GameObject> pool = poolDic[prefab.name];
            GameObject obj = pool.Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))
                CreatePool(key, component.gameObject);

            GameObject obj = poolDic[key].Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    public T Get<T>(T original) where T : Object
    {
        return Get(original, Vector3.zero, Quaternion.identity);
    }

    public bool Release(GameObject obj)
    {
        if (!poolDic.ContainsKey(obj.name))
            return false;

        ObjectPool<GameObject> pool = poolDic[obj.name];
        pool.Release(obj);
        return true;
    }

    private void CreatePool(string key, GameObject prefab)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc : () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.name = key;
                return obj;
            },
            actionOnGet : (GameObject obj) =>
            {
                obj.SetActive(true);
                obj.transform.SetParent(null);
            },
            actionOnRelease : (GameObject obj) =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(transform);
            },
            actionOnDestroy : (GameObject obj) =>
            {
                Destroy(obj);
            }
            );

        poolDic.Add(key, pool);
    }
}
