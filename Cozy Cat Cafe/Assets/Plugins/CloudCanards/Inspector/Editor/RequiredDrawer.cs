using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(RequiredAttribute))]
	public class RequiredDrawer : PropertyDrawer
	{
		private string _message;

		private string Message => _message ?? (_message = ((RequiredAttribute) attribute).Message);

		private static Texture2D _icon;

		private static Texture2D Icon
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
					var content = new GUIContent(Message, Icon);

					var style = EditorStyles.helpBox;
					_messageHeight = style.CalcSize(content).y;
				}

				return _messageHeight.Value;
			}
		}

		private bool ShouldDrawError(SerializedProperty property)
		{
			return property.objectReferenceValue == null;
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
				EditorGUI.HelpBox(position, Message, MessageType.Error);
				
				position.y += MessageHeight + EditorGUIUtility.standardVerticalSpacing;
				position.height = prevHeight - (MessageHeight + EditorGUIUtility.standardVerticalSpacing);
			}

			EditorGUI.PropertyField(position, property, label, true);
		}
	}
}