using System.Collections.Generic;
using UnityEngine;

namespace Core.Utilities
{
	/// <summary>
	/// Managers a dictionary of pools, getting and returning 
	/// </summary>
	public class PoolManager : MonoSingleton<PoolManager>
	{
		private Transform _poolRoot;
		/// <summary>
		/// List of poolables that will be used to initialize corresponding pools
		/// </summary>
		public List<Poolable> poolables;

		/// <summary>
		/// Dictionary of pools, key is the prefab
		/// </summary>
		protected Dictionary<Poolable, AutoComponentPrefabPool<Poolable>> m_Pools = new Dictionary<Poolable, AutoComponentPrefabPool<Poolable>>();

        public Transform PoolRoot { get => _poolRoot; }

        /// <summary>
        /// Gets a poolable component from the corresponding pool
        /// </summary>
        /// <param name="poolablePrefab"></param>
        /// <returns></returns>
        public Poolable GetPoolable(Poolable poolablePrefab)
		{
			if (!m_Pools.ContainsKey(poolablePrefab))
			{
#if DEV_BUILD
				Debug.LogWarning("Poolable not prepared: " + poolablePrefab.name);
#endif
				m_Pools.Add(poolablePrefab, new AutoComponentPrefabPool<Poolable>(poolablePrefab, Initialize, null,
				                                                                  poolablePrefab.initialPoolCapacity));
			}

			AutoComponentPrefabPool<Poolable> pool = m_Pools[poolablePrefab];
			Poolable spawnedInstance = pool.Get();

			spawnedInstance.pool = pool;
			return spawnedInstance;
		}

		public void PreparePool(GameObject prefab)
		{
			Poolable poolable = prefab.GetComponent<Poolable>();

			if (!m_Pools.ContainsKey(poolable))
			{
				m_Pools.Add(poolable, new AutoComponentPrefabPool<Poolable>(poolable, Initialize, null,
																			poolable.initialPoolCapacity));
			}
		}

		/// <summary>
		/// Returns the poolable component to its component pool
		/// </summary>
		/// <param name="poolable"></param>
		public void ReturnPoolable(Poolable poolable)
		{
			poolable.pool.Return(poolable);
		}

        protected override void Awake()
        {
			base.Awake();
			_poolRoot = new GameObject("PoolRoot").transform;
			_poolRoot.SetParent(transform);
		}

        /// <summary>
        /// Initializes the dicionary of pools
        /// </summary>
        protected void Start()
		{
			foreach (var poolable in poolables)
			{
				if (poolable == null)
				{
					continue;
				}
				m_Pools.Add(poolable, new AutoComponentPrefabPool<Poolable>(poolable, Initialize, null,
				                                                            poolable.initialPoolCapacity));
			}
		}

		void Initialize(Component poolable)
		{
			poolable.transform.SetParent(_poolRoot, false);
		}
	}
}