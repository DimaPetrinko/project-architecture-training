using System.Collections.Generic;
using Application;
using Game.GameScene;
using Game.Spawners;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
	public sealed class GameManager : MonoBehaviour
	{
		[SerializeField] private Button exitButton;
		[SerializeField] private TMP_Text scoreText;

		private GameSceneManager currentGameSceneManager;
		private WallSpawner wallSpawner;
		private PlayerView player;
		private List<WallView> walls;
		private uint totalScore;

		private void Awake() => ApplicationManager.Instance.GameManager = this;

		private void Start() => SceneManager.LoadScene(ApplicationManager.Instance.CurrentGameScene.ToString(),
			LoadSceneMode.Additive);

		private void StartGame()
		{
			exitButton.onClick.RemoveAllListeners();
			exitButton.onClick.AddListener(OnExitButtonClicked);

			walls = new List<WallView>();
			wallSpawner = currentGameSceneManager.WallSpawner;
			wallSpawner.ObjectSpawned += OnWallSpawned;
			wallSpawner.StartSpawning(currentGameSceneManager.WallPrefab,
				currentGameSceneManager.WallPrefab.transform.parent);
			
			player = currentGameSceneManager.PlayerView;
			ResetPlayer();
			ResetScore();
		}

		public void RegisterGameSceneManager(GameSceneManager gameSceneManager)
		{
			currentGameSceneManager = gameSceneManager;
			StartGame();
		}

		public Vector3 GetLastWallPosition() => walls.Count > 0 ? walls[walls.Count - 1].transform.position : Vector3.zero;

		private void OnWallSpawned(WallView spawnedObject)
		{
			var wall = spawnedObject.GetComponent<WallView>();
			if (wall == null)
			{
				Debug.LogError($"{spawnedObject.name} that was spawned does not have a {nameof(WallView)} component");
				return;
			}

			walls.Add(wall);
			wall.WallPassed += OnWallPassed;
		}

		private void OnWallPassed(uint score)
		{
			totalScore += score;
			UpdateTotalScoreText();
			Debug.Log($"Scored {score}. Total score = {totalScore}");
		}

		private void ClearWalls()
		{
			walls.ForEach(w =>
			{
				w.WallPassed -= OnWallPassed;
				Destroy(w.gameObject);
			});
			walls.Clear();
		}

		private void ResetPlayer()
		{
			player.transform.position = Vector3.zero; // TODO: Temp. later place him on starting position transform
		}

		private void ResetScore()
		{
			totalScore = 0;
			UpdateTotalScoreText();
		}

		private void OnExitButtonClicked() => SceneManager.LoadScene(ApplicationScenes.MainMenu.ToString());

		private void UpdateTotalScoreText() => scoreText.text = $"Score: {totalScore}";

		private void OnDestroy()
		{
			wallSpawner.StopSpawning();
			wallSpawner.ObjectSpawned -= OnWallSpawned;
			ClearWalls();
			
			ApplicationManager.Instance.GameManager = null;
		}
	}
}