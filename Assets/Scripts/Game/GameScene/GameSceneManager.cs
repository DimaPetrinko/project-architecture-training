using Application;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameScene
{
	public sealed class GameSceneManager : MonoBehaviour
	{
		[SerializeField] private PlayerView playerView;
		[SerializeField] private WallView wallPrefab;

		public PlayerView PlayerView => playerView;
		public WallView WallPrefab => wallPrefab;

		private void Start()
		{
			Debug.Log(gameObject.scene.name);
			SceneManager.SetActiveScene(gameObject.scene);
			ApplicationManager.Instance.GameManager.RegisterGameSceneManager(this);
		}
	}
}