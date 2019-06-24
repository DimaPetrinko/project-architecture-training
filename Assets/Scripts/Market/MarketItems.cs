using System.Collections.Generic;
using UnityEngine;

namespace Market
{
	[CreateAssetMenu]
	public sealed class MarketItems : ScriptableObject
	{
		[SerializeField] private List<MarketItem> list;

		public List<MarketItem> List => list;
	}
}