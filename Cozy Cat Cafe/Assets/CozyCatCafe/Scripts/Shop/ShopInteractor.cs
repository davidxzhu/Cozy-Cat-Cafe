using System.Collections;
using Plugins.CloudCanards.Inspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CozyCatCafe.Scripts.Shop
{
	public class ShopInteractor : MonoBehaviour
	{
		[ScenePicker]
		public string MenuScene;

		private bool _isShopOpen;

		private void RestartScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void ToggleMenu()
		{
			if (_isShopOpen)
				CloseMenu();
			else
				OpenMenu();
		}

		public void OpenMenu()
		{
			StartCoroutine(OpenMenuImpl());
		}

		public void CloseMenu()
		{
			StartCoroutine(CloseMenuImpl());
		}

		private IEnumerator OpenMenuImpl()
		{
			_isShopOpen = true;
			yield return SceneManager.LoadSceneAsync(MenuScene, LoadSceneMode.Additive);
		}

		private IEnumerator CloseMenuImpl()
		{
			_isShopOpen = false;
			yield return SceneManager.UnloadSceneAsync(MenuScene);
		}
		
		#region Singleton

		private static ShopInteractor _instance;

		public static ShopInteractor Instance => _instance;

		private void Awake()
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}

		#endregion
	}
}