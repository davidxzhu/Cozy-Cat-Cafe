using System.Collections;
using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class FoodSource : MonoBehaviour
	{
		[Required]
		public PlayerStats Player;

		[Required]
		public Food FoodToProduce;

		public float Duration;
		private bool _timerStarted;
		private bool _hasFood = true;

		public int PlayerIndex;

		private IEnumerator Timer()
		{
			PlayerVisibility.Instance.Select(PlayerIndex);
			_timerStarted = true;
			yield return new WaitForSeconds(Duration);
			_timerStarted = false;
			_hasFood = true;
		}

		private void OnMouseDown()
		{
			if (_hasFood)
			{
				if (Player.holding == null)
				{
					PlayerVisibility.Instance.Select(PlayerIndex);
					SoundMaster.Play(SoundMaster.Type.Item);
					Player.holding = FoodToProduce;
					//_hasFood = false;
				}
				else
				{
					SoundMaster.Play(SoundMaster.Type.Invalid);
				}
			}
			else
			{
				if (!_timerStarted)
					StartCoroutine(Timer());
				else
					SoundMaster.Play(SoundMaster.Type.Invalid);
			}
		}
	}
}