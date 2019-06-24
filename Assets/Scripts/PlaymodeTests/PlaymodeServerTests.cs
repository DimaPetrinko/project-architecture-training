using System.Collections;
using System.Collections.Generic;
using Application;
using Market;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class PlaymodeServerTests
	{
		[UnityTest]
		public IEnumerator TestGetMarketItems()
		{
			new GameObject("ApplicationManager", typeof(ApplicationManager));
			yield return null;

			ApplicationManager.Instance.MarketItems = Resources.Load<MarketItems>("Configs/MarketItems");
			var server = ApplicationManager.Instance.Server;
			yield return new WaitUntil(() => server.IsInitialized);

			var callbackReceived = false;
			server.GetMarketItems(OnMarketItemsReceived);

			void OnMarketItemsReceived(bool success, List<MarketItem> marketItems)
			{
				callbackReceived = true;
				Assert.True(success);
				Assert.NotNull(marketItems);
				Assert.IsNotEmpty(marketItems);
			}

			yield return new WaitUntil(() => callbackReceived);
		}

		[UnityTest]
		public IEnumerator TestProcessPurchase()
		{
			new GameObject("ApplicationManager", typeof(ApplicationManager));
			yield return null;

			ApplicationManager.Instance.MarketItems = Resources.Load<MarketItems>("Configs/MarketItems");
			var server = ApplicationManager.Instance.Server;
			yield return new WaitUntil(() => server.IsInitialized);

			var callbackReceived = false;
			var item = ApplicationManager.Instance.MarketItems.List[0];
			server.ProcessPurchase(item, OnPurchaseProcessed);

			void OnPurchaseProcessed(bool success)
			{
				callbackReceived = true;
				Assert.True(success);
			}

			yield return new WaitUntil(() => callbackReceived);
		}
		
		[UnityTest]
		public IEnumerator TestRequestCashBalance()
		{
			new GameObject("ApplicationManager", typeof(ApplicationManager));
			yield return null;

			ApplicationManager.Instance.MarketItems = Resources.Load<MarketItems>("Configs/MarketItems");
			var server = ApplicationManager.Instance.Server;
			yield return new WaitUntil(() => server.IsInitialized);

			var callbackReceived = false;
			server.RequestCashBalance(OnCashBalanceReceived);

			void OnCashBalanceReceived(bool success, float balance)
			{
				callbackReceived = true;
				Assert.True(success);
				Assert.GreaterOrEqual(balance, 0f);
			}

			yield return new WaitUntil(() => callbackReceived);
		}
		
		[UnityTest]
		public IEnumerator TestRequestItemInventory()
		{
			new GameObject("ApplicationManager", typeof(ApplicationManager));
			yield return null;

			ApplicationManager.Instance.MarketItems = Resources.Load<MarketItems>("Configs/MarketItems");
			var server = ApplicationManager.Instance.Server;
			yield return new WaitUntil(() => server.IsInitialized);

			var callbackReceived = false;
			server.RequestItemInventory(OnItemInventoryReceived);

			void OnItemInventoryReceived(bool success, List<MarketItem> inventory)
			{
				callbackReceived = true;
				Assert.True(success);
				Assert.NotNull(inventory);
				Assert.IsNotEmpty(inventory);
			}

			yield return new WaitUntil(() => callbackReceived);
		}
	}
}
