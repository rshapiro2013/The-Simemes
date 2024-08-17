using UnityEngine;
using System;

namespace Core.Utilities
{
	/// <summary>
	/// Class that is to be pooled
	/// </summary>
	public class Poolable : MonoBehaviour
	{
		public Action onRepool;
		/// <summary>
		/// Number of poolables the pool will initialize
		/// </summary>
		public int initialPoolCapacity = 10;

		/// <summary>
		/// Pool that this poolable belongs to
		/// </summary>
		public Pool<Poolable> pool;

		/// <summary>
		/// Repool this instance, and move us under the poolmanager
		/// </summary>
		protected virtual void Repool()
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			if (onRepool != null)
			{
				onRepool();
				onRepool = null;
			}
			UnityEngine.Profiling.Profiler.BeginSample("Poolable.Repool", this);
			transform.SetParent(PoolManager.instance.PoolRoot, false);
			UnityEngine.Profiling.Profiler.EndSample();
			pool.Return(this);
		}

		/// <summary>gameObject
		/// Pool the object if possible, otherwise destroy it
		/// </summary>
		/// <param name="gameObject">GameObject attempting to pool</param>
		public static void TryPool(GameObject gameObject)
		{
			var poolable = gameObject.GetComponent<Poolable>();
			if (poolable != null && poolable.pool != null && PoolManager.instanceExists)
			{
				//if (gameObject.name.StartsWith("Projectile_BlackTea"))
				//	Debug.Log("<color=green>TryPool</color> " + gameObject.name + " " + poolable.GetHashCode() + " " + Time.frameCount);
				poolable.Repool();
			}
			else if(poolable != null)
			{
				if (poolable.onRepool != null)
				{
					poolable.onRepool();
					poolable.onRepool = null;
				}
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// If the prefab is poolable returns a pooled object otherwise instantiates a new object
		/// </summary>
		/// <param name="prefab">Prefab of object required</param>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>The pooled or instantiated component</returns>
		public static T TryGetPoolable<T>(GameObject prefab) where T : Component
		{

			var poolable = prefab.GetComponent<Poolable>();
			T instance = poolable != null && PoolManager.instanceExists ?
				PoolManager.instance.GetPoolable(poolable).GetComponent<T>() : Instantiate(prefab).GetComponent<T>();
			//if (prefab.name == "Projectile_BlackTea")
			//	Debug.Log("<color=red>TryGetPoolable</color> " + instance.name + " " + instance.GetHashCode() + " " + Time.frameCount);
			return instance;
		}

		/// <summary>
		/// If the prefab is poolable returns a pooled object otherwise instantiates a new object
		/// </summary>
		/// <param name="prefab">Prefab of object required</param>
		/// <returns>The pooled or instantiated gameObject</returns>
		public static GameObject TryGetPoolable(GameObject prefab)
		{
			var poolable = prefab.GetComponent<Poolable>();
			GameObject instance = poolable != null && PoolManager.instanceExists ?
				PoolManager.instance.GetPoolable(poolable).gameObject : Instantiate(prefab);
			return instance;
		}
	}


	//public abstract class PoolableBase : MonoBehaviour
	//{
	//	protected virtual void Awake()
	//	{
	//		Poolable poolable = GetComponent<Poolable>();
	//		if (poolable != null)
	//			poolable.onRepool += Repool;
	//	}

	//	protected virtual void OnDestroy()
	//	{
	//		Poolable poolable = GetComponent<Poolable>();
	//		if (poolable != null)
	//			poolable.onRepool -= Repool;
	//	}

	//	protected virtual void Repool()
	//	{
	//		transform.localPosition = Vector3.zero;
	//		transform.localRotation = Quaternion.identity;
	//	}
	//}
}