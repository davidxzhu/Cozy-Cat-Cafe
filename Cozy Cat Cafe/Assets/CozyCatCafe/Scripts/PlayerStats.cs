using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class PlayerStats : ScriptableObject
	{
		public int Money;
		public Food holding;
		public bool holdingPlate;
		public Plate holdingDish;
	}
}