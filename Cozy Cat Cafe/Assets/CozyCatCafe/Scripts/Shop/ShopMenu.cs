using UnityEngine;
using UnityEngine.UI;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopMenu : MonoBehaviour
	{
		public HorizontalLayoutGroup Layout;
		public ShopItemPanel ItemPanel;

		public ShopItem[] Items;

		public PlayerStats PlayerStats;

		private void Update()
		{
			InitMenu();
			enabled = false;
		}

		private void InitMenu()
		{
			if (Items == null || Items.Length <= 0 || Layout == null || ItemPanel == null)
				return;

			foreach (Transform o in Layout.transform)
			{
				if (o == null)
					continue;
				
				if (Application.isPlaying)
					Destroy(o.gameObject);
#if UNITY_EDITOR
				else
					DestroyImmediate(o.gameObject);
#endif
			}

			var padding = Layout.padding.left + Layout.padding.right;
			var spacing = Layout.spacing;
			var width = ItemPanel.RectTransform.sizeDelta.x;
			var totalWidth = padding + (width + spacing) * Items.Length - spacing;

			var rectTransform = (RectTransform) Layout.transform;
			var sizeDelta = rectTransform.sizeDelta;
			sizeDelta.x = totalWidth;
			rectTransform.sizeDelta = sizeDelta;

			for (var i = 0; i < Items.Length; i++)
			{
				var obj = Instantiate(ItemPanel, Layout.transform);
				obj.Init(Items[i], this, PlayerStats.Money >= Items[i].Cost);
			}
		}

		public void Buy(ShopItem item)
		{
			PlayerStats.Money -= item.Cost;
			item.OnBought.Invoke();
			ShopInteractor.Instance.CloseMenu();
		}
	}
}