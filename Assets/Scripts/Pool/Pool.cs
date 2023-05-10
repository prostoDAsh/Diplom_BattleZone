using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private const string Name = "Pool";
        private readonly int _countObject;
        
        private readonly T _prefab;
        private readonly GameObject _parent;
        private readonly List<GameObject> _pool = new List<GameObject>(10);

        public GameObject Parent => _parent;

        public Pool(T prefab, int countObject)
        {
            _prefab = prefab;
            _countObject = countObject;
            _parent = new GameObject(Name);
            
            GeneratePool(_prefab.gameObject, countObject);
        }

        private void GeneratePool(GameObject prefab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = Object.Instantiate(prefab, _parent.transform);
                item.SetActive(false);
                _pool.Add(item);
            }
        }

        public bool TryGetObject(out GameObject item)
        {
            item = _pool.FirstOrDefault(ob => ob.activeSelf == false);
            if(item == null) 
                GeneratePool(_prefab.gameObject, _countObject);
            
            return item != null;
        }
        
    }
}