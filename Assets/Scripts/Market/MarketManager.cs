using Application;
using UnityEngine;

namespace Market
{
	public sealed class MarketManager : MonoBehaviour
	{
		private void Awake() => ApplicationManager.Instance.MarketManager = this;
		private void OnDestroy() => ApplicationManager.Instance.MarketManager = null;
	}
}