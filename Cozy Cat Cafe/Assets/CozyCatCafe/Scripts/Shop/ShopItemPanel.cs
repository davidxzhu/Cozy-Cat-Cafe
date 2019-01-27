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

		public Image WasBought;

		public void Init(ShopItem item, ShopMenu menu, bool notExpensive)
		{
			Sprite.sprite = item.Thumbnail;
			Name.text = item.name;
			Description.text = item.Description;
			Cost.text = $"<sprite=1>{item.Cost}";
			BuyButton.interactable = notExpensive;
			BuyButton.onClick.AddListener(() => menu.Buy(item, this));
			if (item.Bought)
				SetBought();
			else
				WasBought.gameObject.SetActive(false);
		}

		public void SetBought()
		{
			BuyButton.interactable = false;
			WasBought.gameObject.SetActive(true);
		}
	}
}