using System.Collections.Generic;
using UnityEngine;

namespace SGGames.Script.Common
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private GameObject m_poolPrefab;
        [SerializeField] private int m_poolSize;
        [SerializeField] private Transform m_poolParent;
        
        private List<GameObject> m_pool;

        private void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            m_pool = new List<GameObject>();
            for (int i = 0; i < m_poolSize; i++)
            {
                var go = Instantiate(m_poolPrefab, m_poolParent);
                go.name += $"_{i}";
                go.SetActive(false);
                m_pool.Add(go);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < m_pool.Count; i++)
            {
                if (!m_pool[i].activeSelf)
                {
                    return m_pool[i];
                }
            }

            return null;
        }

        public void CleanUp()
        {
            foreach (var go in m_pool)
            {
                Destroy(go);
            }

            m_pool.Clear();
        }
    }
}
