using Application;
using UnityEngine;

namespace MainMenu
{
	public sealed class MainMenuManager : MonoBehaviour
	{
		
		private void Awake() => ApplicationManager.Instance.MainMenuManager = this;

		private void OnDestroy() => ApplicationManager.Instance.MainMenuManager = null;
	}
}