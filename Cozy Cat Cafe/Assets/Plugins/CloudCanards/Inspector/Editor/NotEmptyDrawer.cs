using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(NotEmptyAttribute))]
	public class NotEmptyDrawer : PropertyDrawer
	{
		private NotEmptyAttribute _attribute;

		private NotEmptyAttribute Attribute => _attribute ?? (_attribute = attribute as NotEmptyAttribute);

		private Texture2D _icon;

		private Texture2D Icon
		{
			[InternalReflection]
			get
			{
				if (_icon == null)
				{
					var type = typeof(EditorGUIUtility);
					var method = type.GetProperty("errorIcon", BindingFlags.Static | BindingFlags.NonPublic);

					_icon = (Texture2D) method.GetValue(null);
				}

				return _icon;
			}
		}

		private float? _messageHeight;

		private float MessageHeight
		{
			get
			{
				if (_messageHeight == null)
				{
					var content = new GUIContent(Attribute.Message, Icon);

					var style = EditorStyles.helpBox;
					_messageHeight = style.CalcSize(content).y;
				}

				return _messageHeight.Value;
			}
		}

		private bool ShouldDrawError(SerializedProperty property)
		{
			if (property.isArray)
				return property.arraySize <= 0;
			return string.IsNullOrWhiteSpace(property.stringValue);
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
			if (ShouldDrawError(property))
			{
				var prevHeight = position.height;

				position.height = MessageHeight;
				EditorGUI.HelpBox(position, Attribute.Message, MessageType.Error);
				
				position.y += MessageHeight + EditorGUIUtility.standardVerticalSpacing;
				position.height = prevHeight - (MessageHeight + EditorGUIUtility.standardVerticalSpacing);
			}

			EditorGUI.PropertyField(position, property, label, true);
		}

		public override bool CanCacheInspectorGUI(SerializedProperty property)
		{
			return false;
		}
	}
}