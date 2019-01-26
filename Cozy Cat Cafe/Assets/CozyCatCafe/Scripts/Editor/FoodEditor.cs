using CloudCanards.Util;
using UnityEditor;
using UnityEngine;

namespace CozyCatCafe.Scripts.Editor
{
	[CustomEditor(typeof(Food))]
	public class FoodEditor : UnityEditor.Editor
	{
		public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
		{
			return SpriteUtils.RenderStaticPreview((target as Food)?.Sprite, Color.white, width, height);
		}
	}
}