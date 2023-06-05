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

        public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, bool polling = false) where T : Object
        {
            return Instantiate<T>(original, position, rotation, null, polling);
        }

        public new T Instantiate<T>(T original, Transform parent, bool pooling = false) where T : Object
        {
            return Instantiate<T>(original, Vector3.zero, Quaternion.identity, parent, pooling);
        }

        public T Instantiate<T>(T original, bool polling = false) where T : Object
        {
            return Instantiate<T>(original, Vector3.zero, Quaternion.identity, null, polling);
        }

        public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent, bool pooling = false) where T : Object
        {
            T original = Load<T>(path);
            return Instantiate<T>(original, position, rotation, parent, pooling);
        }

        public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, bool polling = false) where T : Object
        {
            return Instantiate<T>(path, position, rotation, null, polling);
        }

        public T Instantiate<T>(string path, Transform parent, bool pooling = false) where T : Object
        {
            return Instantiate<T>(path, Vector3.zero, Quaternion.identity, parent, pooling);
        }

        public T Instantiate<T>(string path, bool pooling = false) where T : Object
        {
            return Instantiate<T>(path, Vector3.zero, Quaternion.identity, null, pooling);
        }

        public void Destroy(GameObject obj)
        {
            if (GameManager.Pool.IsContain(obj))
                GameManager.Pool.Release(obj);
            else
                GameObject.Destroy(obj);
        }

        public void Destroy(GameObject obj, float delay)
        {
            if (GameManager.Pool.IsContain(obj))
                StartCoroutine(DelayReleaseRoutine(obj, delay));
            else
                GameObject.Destroy(obj, delay);
        }

        IEnumerator DelayReleaseRoutine(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            GameManager.Pool.Release(obj);
        }

        public void Destroy(Component component, float delay = 0f)
        {
            Component.Destroy(component, delay);
        }
    }
}
