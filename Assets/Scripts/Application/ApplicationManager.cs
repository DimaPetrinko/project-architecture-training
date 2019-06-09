using Game;
using MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application
{
	public sealed class ApplicationManager : MonoBehaviour
	{
		public static ApplicationManager Instance { get; private set; }

		public MainMenuManager MainMenuManager { get; set; }
		public GameManager GameManager { get; set; }
		
		public ApplicationScenes CurrentGame { get; set; }

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		private void Start() => SceneManager.LoadScene(ApplicationScenes.MainMenu.ToString());
	}
}
