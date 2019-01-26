using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class TrashCan : MonoBehaviour
	{
		[Required]
		public PlayerStats Player;
		private void OnMouseDown()
		{
			if (Player.holding != null)
			{
				Player.holding = null;
				// todo: particles
				Debug.Log("Trashed");
			}
		}
	}
}