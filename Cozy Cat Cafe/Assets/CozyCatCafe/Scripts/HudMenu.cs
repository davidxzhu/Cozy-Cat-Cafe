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

		[ScenePicker]
		public string StartMenu;

		public SaveSystem SaveSystem;

		public Animator animator;

		public void Toggle()
		{
			if (_isOpen)
			{
				_isOpen = false;
				animator.SetBool("clicked", false);
			}
			else
			{
				_isOpen = true;
				animator.SetBool("clicked", true);
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