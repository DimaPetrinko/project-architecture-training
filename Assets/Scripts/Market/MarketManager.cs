using System;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;

namespace Market
{
	public sealed class MarketManager : MonoBehaviour
	{
		[SerializeField] private Transform itemsParent;
		[SerializeField] private MarketItemView itemViewPrefab;
		[SerializeField] private GameObject loadingView;

		private List<MarketItemView> itemViews;
		
		private void Awake() => ApplicationManager.Instance.MarketManager = this;
		private void OnDestroy() => ApplicationManager.Instance.MarketManager = null;

		public void Start()
		{
			itemViews = new List<MarketItemView>();
			itemViews?.ForEach(v =>
			{
				v.MarketItemSelected -= OnMarketItemSelected;
				Destroy(v.gameObject);
			});

			loadingView.SetActive(true);
			ApplicationManager.Instance.Server.GetMarketItems((success, items) =>
			{
				loadingView.SetActive(false);
				itemViewPrefab.gameObject.SetActive(true);
				items.ForEach(i =>
				{
					var view = Instantiate(itemViewPrefab, itemsParent);
					view.MarketItemSelected += OnMarketItemSelected;
					view.Show(i);
					itemViews.Add(view);
				});
				itemViewPrefab.gameObject.SetActive(false);
			});
			
		}

		private void OnMarketItemSelected(MarketItem item, MarketItemView view)
		{
			ApplicationManager.Instance.Server.ProcessPurchase(item, success =>
			{
				if (success)
				{
					itemViews.Remove(view);
					view.MarketItemSelected -= OnMarketItemSelected;
					Destroy(view.gameObject);
				}
				RefreshBalanceText();
			});
		}

		private void RefreshBalanceText()
		{
			ApplicationManager.Instance.Server.RequestCashBalance((success, balance) =>
				{
					balanceText.text = balance.ToString();
				});
		}
		
		private void RefreshInventoryCountText()
		{
			ApplicationManager.Instance.Server.RequestItemInventory((success, inventory) =>
			{
				inventoryCountText.text = inventory.Count.ToString();
			});
		}
	}
	
	public sealed class MarketItemView : MonoBehaviour
	{
		public event Action<MarketItem, MarketItemView> MarketItemSelected;
		
		[SerializeField] private Image icon;
		[SerializeField] private Text nameText;
		[SerializeField] private Text priceText;
		[SerializeField] private Button button;

		public void Show(MarketItem item)
		{
			icon.sprite = item.Icon;
			priceText.text = item.Price.ToString();
			
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => MarketItemSelected?.Invoke(item, this));
		}
	}
}