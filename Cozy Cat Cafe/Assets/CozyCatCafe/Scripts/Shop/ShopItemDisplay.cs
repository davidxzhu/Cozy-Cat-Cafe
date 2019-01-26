using UnityEngine;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopItemDisplay : MonoBehaviour
	{
		public ShopItem Item;

		private void Awake()
		{
			enabled = false;
			Item.OnBought.AddListener(() => enabled = true);
		}
	}
}