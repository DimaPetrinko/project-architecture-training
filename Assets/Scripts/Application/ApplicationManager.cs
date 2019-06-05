using System;
using UnityEngine;
using UnityEngine.UI;

namespace Application
{
	public class ApplicationManager : MonoBehaviour
	{
		public event Action ButtonClicked;
		
		[SerializeField] private Button someButton;
		[SerializeField] private Image image;
		
		public MainMenuManager MainMenuManager { get; }

		private void Awake()
		{
			DontDestroyOnLoad(this);
			
			someButton.onClick.AddListener(OnSomeButtonClicked);

			ButtonClicked += OnButtonClicked;
		}

		private void OnButtonClicked()
		{
			// always unsubscribe from the event
			image.color = randomColor;
		}
		
		private void OnSomeButtonClicked()
		{
			ButtonClicked?.Invoke();
		}

		private void OnDestroy()
		{
			ButtonClicked -= OnButtonClicked;
		}
	}
}
