using UnityEngine;
using UnityEngine.Events;

namespace CozyCatCafe.Scripts.Shop
{
	[CreateAssetMenu(menuName = "Shop Item")]
	public class ShopItem : ScriptableObject
	{
		public string Name;
		[TextArea]
		public string Description;

		public Sprite Thumbnail;

		public int Cost = 1;

		public UnityEvent OnBought;
	}
}