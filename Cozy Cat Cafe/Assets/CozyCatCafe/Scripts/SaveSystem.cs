using System.Text;
using CozyCatCafe.Scripts.Shop;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class SaveSystem : MonoBehaviour
	{
		private const string MoneyKey = "Money";
		private const string DecorationsKey = "Decorations";
		
		public PlayerStats Player;

		public ShopItem[] Decorations;

		private string _decorationsDefaultValue;

		private void Awake()
		{
			_decorationsDefaultValue = new string('f', Decorations.Length);
		}

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
			var decorationsStr = PlayerPrefs.GetString(DecorationsKey, _decorationsDefaultValue);
			for (var index = 0; index < Decorations.Length; index++)
			{
				Decorations[index].Bought = decorationsStr[index] == 't';
			}
		}
	}
}