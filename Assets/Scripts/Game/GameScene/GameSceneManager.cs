using Application;
using Game.Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameScene
{
	public sealed class GameSceneManager : MonoBehaviour
	{
		[SerializeField] private Camera gameCamera;
		[SerializeField] private PlayerView playerView;
		[SerializeField] private WallView wallPrefab;
		[SerializeField] private WallSpawner wallSpawner;

		public Camera GameCamera => gameCamera;
		public PlayerView PlayerView => playerView;
		public WallView WallPrefab => wallPrefab;
		public WallSpawner WallSpawner => wallSpawner;

		private void Start()
		{
			Debug.Log(gameObject.scene.name);
			SceneManager.SetActiveScene(gameObject.scene);
			ApplicationManager.Instance.GameManager.RegisterGameSceneManager(this);
		}
	}
}