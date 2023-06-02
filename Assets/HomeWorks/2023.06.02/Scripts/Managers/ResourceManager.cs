using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork0602
{
    public class ResourceManager : MonoBehaviour
    {
        Dictionary<string, Object> resources = new Dictionary<string, Object>();

        public T Load<T>(string path) where T : Object
        {
            string key = $"{typeof(T)}.{path}";

            if (resources.ContainsKey(key))
                return resources[key] as T;

            T resource = Resources.Load<T>(path);
            resources.Add(key, resource);
            return resource;
        }

        public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent, bool pooling = false) where T : Object
        {
            if (pooling)
                return GameManager.Pool.Get(original, position, rotation, parent);
            else
                return Object.Instantiate(original, position, rotation, parent);
        }

        public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent, bool pooling = false) where T : Object
        {
            T original = Load<T>(path);
            return Instantiate<T>(original, position, rotation, parent, pooling);
        }

        public void Destroy(GameObject obj)
        {
            if (GameManager.Pool.Release(obj))
                return;

            GameObject.Destroy(obj);
        }
    }
}
