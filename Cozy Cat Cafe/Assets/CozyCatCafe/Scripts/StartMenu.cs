using Plugins.CloudCanards.Inspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CozyCatCafe.Scripts
{
	public class StartMenu : MonoBehaviour
	{
		[ScenePicker]
		public string PlayScene;
		public SaveSystem Save;

		public void NewGamePressed()
		{
			SceneManager.LoadScene(PlayScene);
		}

		public void ContinuePressed()
		{
			Save.Load();
			NewGamePressed();
		}

		public void QuitPressed()
		{
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