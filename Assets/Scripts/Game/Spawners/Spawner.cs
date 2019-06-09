using System;
using System.Collections;
using UnityEngine;

namespace Game.Spawners
{
	public abstract class Spawner<T> : MonoBehaviour where T : Component
	{
		public event Action<T> ObjectSpawned;

		[SerializeField] private float spawnRate;

		public void StartSpawning(T prefab, Transform parent = null)
		{
			if (parent == null) parent = transform;
			StartCoroutine(Spawn(prefab, parent));
		}

		public void StopSpawning() => StopAllCoroutines();

		private IEnumerator Spawn(T prefab, Transform parent)
		{
			while (true) // timer
			{
				// spawns a new prefab based on rate
				T newObject = null;
				ObjectSpawned?.Invoke(newObject);
				yield return null;
			}
		}
	}
}