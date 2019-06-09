using Application;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
	public sealed class MainMenuManager : MonoBehaviour
	{
		[SerializeField] private Button playButton;
		
		private void Awake() => ApplicationManager.Instance.MainMenuManager = this;

		private void Start()
		{
			playButton.onClick.RemoveAllListeners();
			playButton.onClick.AddListener(OnPlayButton);
		}

		private void OnPlayButton() => SceneManager.LoadScene(ApplicationScenes.Game.ToString());

		private void OnDestroy() => ApplicationManager.Instance.MainMenuManager = null;
	}
}