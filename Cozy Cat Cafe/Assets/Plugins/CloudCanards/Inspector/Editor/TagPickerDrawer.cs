using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(TagPickerAttribute))]
	public class TagPickerDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				if (string.IsNullOrEmpty(property.stringValue))
					property.stringValue = "Untagged";
				property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
			}
			else
				EditorGUI.LabelField(position, label.text, "Use [Scene] with strings.");
		}
	}
}