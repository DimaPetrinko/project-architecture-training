using System;
using System.Collections.Generic;
using Market;

namespace Servers
{
	public interface IServer
	{
		bool IsInitialized { get; }
		
		void Initialize(Action<string> callback);
		void GetMarketItems(Action<bool, List<MarketItem>> callback);
		void ProcessPurchase(MarketItem item, Action<bool> callback);
		void RequestCashBalance(Action<bool, float> callback);
		void RequestItemInventory(Action<bool, List<MarketItem>> callback);
	}
}