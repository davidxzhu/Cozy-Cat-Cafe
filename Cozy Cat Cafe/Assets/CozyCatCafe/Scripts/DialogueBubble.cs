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
				Renderer.enabled = false;
			}
			else
			{
				Renderer.enabled = true;
				Renderer.sprite = sprite;
			}
		}
	}
}