using System;
using System.Collections;
using System.Collections.Generic;
using Application;
using Market;
using UnityEngine;

namespace Servers
{
	public abstract class BaseServer : IServer
	{
		private const float SIMULATED_WAIT_TIME = 1f;

		private WaitForSeconds simulationWaitForSeconds;
		
		public bool IsInitialized { get; protected set; }

		public virtual void Initialize(Action<string> callback) =>
			simulationWaitForSeconds = new WaitForSeconds(SIMULATED_WAIT_TIME);
		public abstract void GetMarketItems(Action<bool, List<MarketItem>> callback);
		public abstract void ProcessPurchase(MarketItem item, Action<bool> callback);
		public abstract void RequestCashBalance(Action<bool, float> callback);
		public abstract void RequestItemInventory(Action<bool, List<MarketItem>> callback);
		protected void SimulateDelay(Action action) =>
			ApplicationManager.Instance.StartCoroutine(WaitCoroutine(action));

		private IEnumerator WaitCoroutine(Action action)
		{
			yield return simulationWaitForSeconds;
			action?.Invoke();
		}
	}
}