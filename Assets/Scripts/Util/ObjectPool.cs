using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string _tag;
        public GameObject _prefab;
        public int _size;
    }

    public List<Pool> _pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in _pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool._size; ++i)
            {
                GameObject obj = Instantiate(pool._prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            _poolDictionary.Add(pool._tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if(!_poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = _poolDictionary[tag].Dequeue();
        _poolDictionary[tag].Enqueue(obj);

        obj.SetActive(true);
        return obj; ;
    }
}
