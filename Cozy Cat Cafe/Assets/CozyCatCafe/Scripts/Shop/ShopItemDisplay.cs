using UnityEngine;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopItemDisplay : MonoBehaviour
	{
		public ShopItem Item;
		
		private void Awake()
		{
			gameObject.SetActive(Item.Bought);
			Item.OnBought.AddListener(() => gameObject.SetActive(true));
		}
	}
}