using UnityEngine;
using UnityEngine.UI;

namespace CozyCatCafe.Scripts
{
	public class ProgressBar : MonoBehaviour
	{
		public Image Image;

		public float Progress
		{
			set => Image.fillAmount = value;
		}

		private void Awake()
		{
			Progress = 0f;
		}
	}
}