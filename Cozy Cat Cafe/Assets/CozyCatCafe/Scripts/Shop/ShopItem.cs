using System;
using UnityEngine;
using UnityEngine.Events;

namespace CozyCatCafe.Scripts.Shop
{
	[CreateAssetMenu(menuName = "Shop Item")]
	public class ShopItem : ScriptableObject
	{
		public Sprite Thumbnail;

		public int Cost = 1;

		[NonSerialized]
		public bool Bought;

		public UnityEvent OnBought;
	}
}