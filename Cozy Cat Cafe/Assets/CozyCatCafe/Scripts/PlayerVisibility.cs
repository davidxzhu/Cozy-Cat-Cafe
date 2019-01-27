using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class PlayerVisibility : MonoBehaviour
	{
		public static PlayerVisibility Instance { get; private set; }

		public GameObject[] Players;

		private void Awake()
		{
			Instance = this;

			var index = Random.Range(0, Players.Length);
			Select(index);
		}

		public void Select(int index)
		{
			for (var i = 0; i < Players.Length; i++)
			{
				Players[i].SetActive(i == index);
			}
		}
	}
}