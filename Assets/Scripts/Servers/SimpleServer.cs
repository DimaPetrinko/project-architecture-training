using System;
using System.Collections.Generic;
using Application;
using Market;
using UnityEngine;

namespace Servers
{
	public sealed class SimpleServer : BaseServer
	{
		public override void Initialize(Action<string> callback)
		{
			base.Initialize(callback);
			
			var sessionId = Guid.NewGuid().ToString();

			var accountData = HasData(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY)
				? LoadData<AccountData>(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY)
				: new AccountData(100f);

			SaveData(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY, accountData);

			IsInitialized = true;

			SimulateDelay(() => callback(sessionId));
		}

		public override void GetMarketItems(Action<bool, List<MarketItem>> callback) // returns a list of all available
																					 // items from config and bool success
		{
			var success = false;
			List<MarketItem> items = null;

			if (IsInitialized)
			{
				var applicationManager = ApplicationManager.Instance;
				if (applicationManager != null)
				{
					var marketItems = applicationManager.MarketItems;
					if (marketItems != null)
					{
						if (marketItems.List != null && marketItems.List.Count > 0)
						{
							success = true;
							items = marketItems.List;
						}
					}
				}
			}

			SimulateDelay(() => callback(success, items));
		}

		public override void ProcessPurchase(MarketItem item, Action<bool> callback) // subtracts money from balance
																					 // and adds item to inventory
																					 // return bool success via callback
		{
			var success = false;

			if (IsInitialized)
			{
				var accountData = LoadData<AccountData>(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY);
				if (!accountData.Equals(default))
				{
					var canPurchase = accountData.Balance >= item.Price;
					if (canPurchase)
					{
						success = true;
						accountData.Balance -= item.Price;
						if (accountData.Inventory == null) accountData.Inventory = new List<MarketItem>();
						accountData.Inventory.Add(item);
						SaveData(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY, accountData);
					}
				}
			}

			SimulateDelay(() => callback(success));
		}

		public override void RequestCashBalance(Action<bool, float> callback) // returns bool success and balance from
																			  // PlayerPrefs via callback
		{
			var success = false;
			var balance = -1f;

			if (IsInitialized)
			{
				var accountData = LoadData<AccountData>(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY);
				if (!accountData.Equals(default))
				{
					success = true;
					balance = accountData.Balance;
				}
			}

			SimulateDelay(() => callback(success, balance));
		}

		public override void RequestItemInventory(Action<bool, List<MarketItem>> callback) // you get the idea
		{
			var success = false;
			List<MarketItem> inventory = null;

			if (IsInitialized)
			{
				var accountData = LoadData<AccountData>(ApplicationConstants.ACCOUNT_DATA_SAVE_KEY);
				if (!accountData.Equals(default))
				{
					success = true;
					inventory = accountData.Inventory;
				}
			}

			SimulateDelay(() => callback(success, inventory));
		}

		private static bool HasData(string saveKey) => PlayerPrefs.HasKey(saveKey);

		private static void SaveData(string saveKey, object data)
		{
			var dataJson = JsonUtility.ToJson(data);
			PlayerPrefs.SetString(saveKey, dataJson);
		}

		private static T LoadData<T>(string saveKey)
		{
			var dataJson = PlayerPrefs.GetString(saveKey);
			var data = JsonUtility.FromJson<T>(dataJson);
			return data;
		}

		private static void RemoveAllData() => PlayerPrefs.DeleteAll();
	}
}