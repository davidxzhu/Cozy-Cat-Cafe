using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector.Editor
{
	[CustomPropertyDrawer(typeof(FunctionButtonAttribute))]
	public class FunctionButtonDrawer : PropertyDrawer
	{
		private FunctionButtonAttribute _typedAttribute;

		private FunctionButtonAttribute TypedAttribute =>
			_typedAttribute ?? (_typedAttribute = attribute as FunctionButtonAttribute);

		private float? _buttonHeight;

		private float ButtonHeight
		{
			get
			{
				if (_buttonHeight == null)
				{
					var content = new GUIContent(TypedAttribute.ButtonName);

					var style = GUI.skin.box;
					style.alignment = TextAnchor.MiddleCenter;

					// Compute how large the button needs to be
					_buttonHeight = style.CalcSize(content).y;
				}

				return _buttonHeight.Value;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true) + ButtonHeight +
			       EditorGUIUtility.standardVerticalSpacing;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (TypedAttribute.PlaceAboveField)
			{
				var height = position.height;
				DrawButton(position, property);
				position.y += EditorGUIUtility.standardVerticalSpacing + ButtonHeight;
				position.height = height - (EditorGUIUtility.standardVerticalSpacing + ButtonHeight);
			}

			EditorGUI.PropertyField(position, property, label, true);
			position.y += EditorGUI.GetPropertyHeight(property, label, true);

			if (!TypedAttribute.PlaceAboveField)
			{
				position.y += EditorGUIUtility.standardVerticalSpacing;
				DrawButton(position, property);
			}
		}

		private void DrawButton(Rect position, SerializedProperty property)
		{
			var disabled = false;
			if (TypedAttribute.OnlyDuringPlayMode && !Application.isPlaying)
			{
				EditorGUI.BeginDisabledGroup(true);
				disabled = true;
			}

			position.height = ButtonHeight;
			if (GUI.Button(position, TypedAttribute.ButtonName))
			{
				var objectReferenceValue = property.serializedObject.targetObject;
				var type = objectReferenceValue.GetType();
				const BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Instance |
				                                 BindingFlags.Public | BindingFlags.NonPublic;

				try
				{
					var method = type.GetMethod(TypedAttribute.FunctionName, bindingAttr);
					method.Invoke(method.IsStatic ? null : objectReferenceValue, null);
				}
				catch (AmbiguousMatchException)
				{
					var function = $"{type.Name}.{TypedAttribute.FunctionName}";
					var message = $"{function} : AmbiguousMatchException. " +
					              $"Unable to determine which overloaded function is called for {function}. " +
					              "Please delete overloading function";

					Debug.LogError(message, objectReferenceValue);
				}
				catch (ArgumentException)
				{
					var function = $"{type.Name}.{TypedAttribute.FunctionName}";
					var message = $"{function} : ArgumentException. " +
					              $"You can't pass arguments to the function {function}. " +
					              "Please verify the types of the arguments";

					Debug.LogError(message, objectReferenceValue);
				}
				catch (NullReferenceException)
				{
					var function = $"{type.Name}.{TypedAttribute.FunctionName}";
					var message = $"{function} : NullReferenceException. " +
					              $"Undefined function. Please verify if function is defined in {function}";

					Debug.LogError(message, objectReferenceValue);
				}
			}

			if (disabled)
				EditorGUI.EndDisabledGroup();
		}
	}
}