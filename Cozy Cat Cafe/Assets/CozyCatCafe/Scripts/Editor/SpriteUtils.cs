using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CloudCanards.Util
{
	public static class SpriteUtils
	{
#if UNITY_EDITOR
		private static readonly System.Reflection.MethodInfo RenderStaticPreviewMethod;
		
		[InternalReflection]
		static SpriteUtils()
		{
			var assembly = typeof(UnityEditor.Editor).Assembly;
			var type = assembly.GetType("UnityEditor.SpriteUtility");
			RenderStaticPreviewMethod = type?.GetMethod("RenderStaticPreview",
				System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null,
				System.Reflection.CallingConventions.Any,
				new[] {typeof(Sprite), typeof(Color), typeof(int), typeof(int)}, null);
			if (RenderStaticPreviewMethod == null)
			{
				Debug.LogError("Reflection failed");
			}
		}
#endif

		public static Texture2D RenderStaticPreview(Sprite sprite, Color color, int width, int height)
		{
			if (sprite == null)
				return null;

#if UNITY_EDITOR
			if (RenderStaticPreviewMethod != null)
			{
				var texture = RenderStaticPreviewMethod.Invoke(null, new object[] {sprite, color, width, height});
				return texture as Texture2D;
			}
#endif
			return null;
		}
	}
}