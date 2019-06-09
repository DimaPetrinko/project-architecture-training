using System;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(Rigidbody2D))]
	public sealed class WallView : MonoBehaviour
	{
		public event Action<uint> WallPassed;

		[SerializeField] private uint score;
		
		private void Awake()
		{
			// Gets its position from game manager
			// Randomly chooses the hole size and its vertical position
		}

		private void OnTriggerExit(Collider other) => WallPassed?.Invoke(score);
	}
}