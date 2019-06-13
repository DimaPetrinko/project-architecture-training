using Game;
using MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application
{
	public sealed class ApplicationManager : MonoBehaviour
	{
		private static ApplicationManager instance;

		public static ApplicationManager Instance
		{
			get
			{
				if (instance == null) SceneManager.LoadScene(ApplicationScenes.Application.ToString());
				return instance;
			}
			private set => instance = value;
		}

		public MainMenuManager MainMenuManager { get; set; }
		public GameManager GameManager { get; set; }
		
		public ApplicationScenes CurrentGameScene { get; set; }

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		private void Start() => SceneManager.LoadScene(ApplicationScenes.MainMenu.ToString());
	}
}
