using System.Collections;
using CozyCatCafe.Scripts.Shop;
using Plugins.CloudCanards.Inspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CozyCatCafe.Scripts
{
	public class HudMenu : MonoBehaviour
	{
		private static readonly int OpenAnim = Animator.StringToHash("Open");
		private static readonly int CloseAnim = Animator.StringToHash("Close");

		public RectTransform[] Buttons;

		public float PopDuration = 0.1f;

		private bool _isOpen;

		public Animator Animation;

		[ScenePicker]
		public string StartMenu;

		public SaveSystem SaveSystem;

		private IEnumerator ButtonPopIn()
		{
			for (var i = 0; i < Buttons.Length; i++)
			{
				yield return new WaitForSecondsRealtime(PopDuration);
				Buttons[i].transform.localScale = new Vector3(1, 1, 1);
			}
		}

		private IEnumerator ButtonPopOut()
		{
			for (var i = 0; i < Buttons.Length; i++)
			{
				yield return new WaitForSecondsRealtime(PopDuration);
				Buttons[i].transform.localScale = new Vector3(0, 0, 1);
			}
		}

		public void Toggle()
		{
			if (_isOpen)
			{
				_isOpen = false;
				StartCoroutine(ButtonPopOut());
				Animation.Play(CloseAnim);
			}
			else
			{
				_isOpen = true;
				StartCoroutine(ButtonPopIn());
				Animation.Play(OpenAnim);
			}
		}

		public void OpenShopPressed()
		{
			Toggle();
			ShopInteractor.Instance.OpenMenu();
		}

		public void SavePressed()
		{
			Toggle();
			SaveSystem.Save();
		}

		public void ClosePressed()
		{
			SceneManager.LoadScene(StartMenu);
		}
	}
}