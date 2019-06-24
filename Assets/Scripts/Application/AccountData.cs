using System;
using System.Collections.Generic;
using Market;
using UnityEngine;

namespace Application
{
	[Serializable]
	public struct AccountData
	{
		[SerializeField] private float balance;
		[SerializeField] private List<MarketItem> inventory;

		public float Balance
		{
			get => balance;
			set => balance = value;
		}

		public List<MarketItem> Inventory
		{
			get => inventory;
			set => inventory = value;
		}

		public AccountData(float balance) : this()
		{
			Balance = balance;
			Inventory = new List<MarketItem>();
		}
	}
}