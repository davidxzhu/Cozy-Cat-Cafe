using System.Text;
using CozyCatCafe.Scripts.Shop;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class SaveSystem : ScriptableObject
	{
		private const string MoneyKey = "Money";
		private const string DecorationsKey = "Decorations";
		
		public PlayerStats Player;

		public ShopItem[] Decorations;

		public void Save()
		{
			PlayerPrefs.SetInt(MoneyKey, Player.Money);
			
			// decorations
			var sb = new StringBuilder();
			foreach (var decoration in Decorations)
			{
				sb.Append(decoration.Bought ? 't' : 'f');
			}
			PlayerPrefs.SetString(DecorationsKey, sb.ToString());
			
			// save
			PlayerPrefs.Save();
		}

		public void Load()
		{
			PlayerPrefs.GetInt(MoneyKey, Player.Money);
			
			// decorations
			var decorationsStr = PlayerPrefs.GetString(DecorationsKey, new string('f', Decorations.Length));
			for (var index = 0; index < Decorations.Length; index++)
			{
				Decorations[index].Bought = decorationsStr[index] == 't';
			}
		}

		public void Reset()
		{
			Player.Money = 0;
			Player.holding = null;
			
			foreach (var shopItem in Decorations)
			{
				shopItem.Bought = false;
			}
		}
	}
}