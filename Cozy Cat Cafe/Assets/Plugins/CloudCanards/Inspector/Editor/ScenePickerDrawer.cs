using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(ScenePickerAttribute))]
	public class ScenePickerDrawer : PropertyDrawer
	{
		private const string NotInBuildMessage = "Scene not included in build settings!";
		
		private static Texture2D _icon;

		private static Texture2D Icon
		{
			[InternalReflection]
			get
			{
				if (_icon == null)
				{
					var type = typeof(EditorGUIUtility);
					var method = type.GetProperty("warningIcon", BindingFlags.Static | BindingFlags.NonPublic);

					_icon = (Texture2D) method.GetValue(null);
				}

				return _icon;
			}
		}
		
		private static float? _messageHeight;

		private static float MessageHeight
		{
			get
			{
				if (_messageHeight == null)
				{
					var content = new GUIContent(NotInBuildMessage, Icon);

					var style = EditorStyles.helpBox;
					_messageHeight = style.CalcSize(content).y;
				}

				return _messageHeight.Value;
			}
		}

		private bool ShouldDrawError(SerializedProperty property)
		{
			if (string.IsNullOrWhiteSpace(property.stringValue))
				return true;
			foreach (var editorScene in EditorBuildSettings.scenes)
			{
				if (editorScene.path == property.stringValue)
				{
					return false;
				}
			}
			return true;
		}
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var height = EditorGUI.GetPropertyHeight(property, label, true);
			if (ShouldDrawError(property))
			{
				height += MessageHeight + EditorGUIUtility.standardVerticalSpacing;
			}
			return height;
		}
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				if (ShouldDrawError(property))
				{
					var prevHeight = position.height;

					position.height = MessageHeight;
					EditorGUI.HelpBox(position, NotInBuildMessage, MessageType.Warning);
				
					position.y += MessageHeight + EditorGUIUtility.standardVerticalSpacing;
					position.height = prevHeight - (MessageHeight + EditorGUIUtility.standardVerticalSpacing);
				}
				
				EditorGUI.BeginChangeCheck();
				var sceneObject = GetSceneObject(property.stringValue);
				var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
				if (EditorGUI.EndChangeCheck())
				{
					if (scene == null)
					{
						property.stringValue = null;
					}
					else
					{
						var newPath = AssetDatabase.GetAssetPath(scene);
						property.stringValue = newPath;
					}
				}
			}
			else
				EditorGUI.LabelField(position, label.text, "Use [ScenePicker] with strings.");
		}

		private static SceneAsset GetSceneObject(string sceneObjectName)
		{
			if (string.IsNullOrEmpty(sceneObjectName))
			{
				return null;
			}

			var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneObjectName);
			return scene;
		}
	}
}