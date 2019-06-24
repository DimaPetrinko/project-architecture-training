using MainMenu;
using Market;
using Servers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application
{
	public sealed class ApplicationManager : MonoBehaviour
	{
		[SerializeField] private MarketItems marketItems;
		
		public static ApplicationManager Instance { get; private set; }

		public IServer Server { get; private set; }
		public MainMenuManager MainMenuManager { get; set; }
		public MarketManager MarketManager { get; set; }

		public MarketItems MarketItems
		{
			get => marketItems;
			set => marketItems = value;
		}

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		private void Start()
		{
			Debug.Log("Starting server");
			Server = new SimpleServer();
			Server.Initialize(OnServerInitialized);

			void OnServerInitialized(string sessionId)
			{
				Debug.Log($"Server initialized and returned session id {sessionId}");
				SceneManager.LoadScene(ApplicationScenes.MainMenu.ToString());
			}
		}
	}
}
