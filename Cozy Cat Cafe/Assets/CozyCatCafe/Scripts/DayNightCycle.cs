using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CozyCatCafe.Scripts
{
	public class DayNightCycle : MonoBehaviour
	{
		public PostProcessProfile Profile;
		private ColorGrading _colorGrading;

		[Range(0f, 1f)]
		public float CurrentTime;
		public float Duration = 10 * 60f;

		public AnimationCurve Temperature;
		public AnimationCurve Brightness;

		private void Awake()
		{
			_colorGrading = Profile.GetSetting<ColorGrading>();
			CurrentTime = Random.Range(0f, 1f);
		}

		private void Update()
		{
			CurrentTime = (CurrentTime + Time.deltaTime / Duration) % 1f;

			_colorGrading.temperature.value = Temperature.Evaluate(CurrentTime);
			_colorGrading.brightness.value = Brightness.Evaluate(CurrentTime);
		}
	}
}