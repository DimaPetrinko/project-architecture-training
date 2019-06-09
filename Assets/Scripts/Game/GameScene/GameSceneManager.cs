using Application;
using UnityEngine;

namespace Game.GameScene
{
	public sealed class GameSceneManager : MonoBehaviour
	{
		[SerializeField] private PlayerView playerView;
		[SerializeField] private WallView wallPrefab;

		public PlayerView PlayerView => playerView;
		public WallView WallPrefab => wallPrefab;

		private void Awake()
		{
			ApplicationManager.Instance.GameManager.RegisterGameSceneManager(this);
		}
	}
}