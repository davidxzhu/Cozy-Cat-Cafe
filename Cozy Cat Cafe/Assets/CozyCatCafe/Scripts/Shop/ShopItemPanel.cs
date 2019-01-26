using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopItemPanel : MonoBehaviour
	{
		public RectTransform RectTransform;
		public Image Sprite;
		public TextMeshProUGUI Name;
		public TextMeshProUGUI Description;
		public TextMeshProUGUI Cost;
		public Button BuyButton;

		public void Init(ShopItem item, ShopMenu menu, bool canBuy)
		{
			Sprite.sprite = item.Thumbnail;
			Name.text = item.Name;
			Description.text = item.Description;
			Cost.text = $"<sprite=1>{item.Cost}";
			BuyButton.interactable = canBuy;
			BuyButton.onClick.AddListener(() => menu.Buy(item));
		}
	}
}