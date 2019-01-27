using JetBrains.Annotations;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class DialogueBubble : MonoBehaviour
	{
		public SpriteRenderer Renderer;

		private void Awake()
		{
			ChangeSprite(null);
		}

		public void ChangeSprite([CanBeNull] Sprite sprite)
		{
			if (sprite == null)
			{
				Renderer.sprite = null;
				gameObject.SetActive(false);
			}
			else
			{
				Renderer.sprite = sprite;
				gameObject.SetActive(true);
			}
		}
	}
}