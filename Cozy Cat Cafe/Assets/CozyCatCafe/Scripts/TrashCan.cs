using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class TrashCan : MonoBehaviour
	{
		[Required]
		public PlayerStats Player;
		public int PlayerIndex;
		private void OnMouseDown()
		{
			PlayerVisibility.Instance.Select(PlayerIndex);
			if (Player.holding != null)
			{
				Player.holding = null;
				// todo: particles
				var sound = GetComponent<AudioSource>();
				if (sound != null)
					sound.Play();
			}
			else
			{
				SoundMaster.Play(SoundMaster.Type.Invalid);
			}
		}
	}
}