using System;
using UnityEngine;

namespace Market
{
	[Serializable]
	public struct MarketItem
	{
		[SerializeField] private float price;
		[SerializeField] private Sprite icon;
		public float Price => price;
		public Sprite Icon => icon;
	}
}