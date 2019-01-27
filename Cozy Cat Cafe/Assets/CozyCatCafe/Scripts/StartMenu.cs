using Plugins.CloudCanards.Inspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CozyCatCafe.Scripts
{
	public class StartMenu : MonoBehaviour
	{
		[ScenePicker]
		public string PlayScene;
		public SaveSystem Save;

		public Button QuitButton;

		private void Awake()
		{
#if UNITY_WEBGL
			QuitButton.interactable = false;
#endif
		}

		public void NewGamePressed()
		{
			SoundMaster.Play(SoundMaster.Type.Menu);
			SceneManager.LoadScene(PlayScene);
		}

		public void ContinuePressed()
		{
			Save.Load();
			NewGamePressed();
		}

		public void QuitPressed()
		{
			SoundMaster.Play(SoundMaster.Type.Menu);
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
			Debug.Log("Can't just quit in web");
#else
			Application.Quit();
#endif
		}
	}
}