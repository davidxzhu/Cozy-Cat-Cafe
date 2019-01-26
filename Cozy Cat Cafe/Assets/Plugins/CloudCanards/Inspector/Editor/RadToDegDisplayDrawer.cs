using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(RadToDegDisplayAttribute))]
	public class RadToDegDisplayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.Float)
				property.floatValue = EditorGUI.Slider(position, label, property.floatValue * Mathf.Rad2Deg, 0, 360) *
				                      Mathf.Deg2Rad;
			else
				EditorGUI.LabelField(position, label.text, "Use [RadToDegDisplay] with floats.");
		}
	}
}