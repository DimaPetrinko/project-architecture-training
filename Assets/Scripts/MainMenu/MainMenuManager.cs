using System;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
	public sealed class MainMenuManager : MonoBehaviour
	{
		[SerializeField] private List<Button> gameSceneButtons;
		
		private void Awake() => ApplicationManager.Instance.MainMenuManager = this;

		private void Start()
		{
			for (int i = 0; i < gameSceneButtons.Count; i++)
			{
				var gameSceneIndex = i;
				gameSceneButtons[gameSceneIndex].onClick.RemoveAllListeners();
				gameSceneButtons[gameSceneIndex].onClick.AddListener(() => SelectGameScene(gameSceneIndex));
			}
		}

		private void SelectGameScene(int gameSceneIndex)
		{
			ApplicationScenes selectedScene;
			switch (gameSceneIndex)
			{
				case 0: selectedScene = ApplicationScenes.GameScene1; break;
				case 1: selectedScene = ApplicationScenes.GameScene2; break;
				case 2: selectedScene = ApplicationScenes.GameScene3; break;
				default: throw new ArgumentOutOfRangeException();
			}

			ApplicationManager.Instance.CurrentGameScene = selectedScene;
			LoadGameScene();
		}
		
		private void LoadGameScene() => SceneManager.LoadScene(ApplicationScenes.Game.ToString());

		private void OnDestroy() => ApplicationManager.Instance.MainMenuManager = null;
	}
}