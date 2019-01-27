using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class SingleMusic : MonoBehaviour
	{
		private static bool Exists;

		private void Awake()
		{
			if (Exists)
				Destroy(gameObject);
			else
			{
				Exists = true;
				DontDestroyOnLoad(this);
			}
		}
	}
}