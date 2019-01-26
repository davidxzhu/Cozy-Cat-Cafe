using UnityEngine;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopMenu : MonoBehaviour
	{
		public ShopItem[] Items;

		private void Awake()
		{
			InitMenu();
		}

		private void InitMenu()
		{
			if (Items == null || Items.Length <= 0)
				return;
			
			
		}
		
		public void Buy(ShopItem item)
		{
			
		}

		private void OnValidate()
		{
			InitMenu();
		}
	}
}