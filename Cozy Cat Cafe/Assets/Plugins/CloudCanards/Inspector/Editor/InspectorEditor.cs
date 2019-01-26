using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.CloudCanards.Inspector.Editor
{
	/// <summary>
	/// Handles foldout groups, buttons at the end
	/// </summary>
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Object), true)]
	public class InspectorEditor : UnityEditor.Editor
	{
		private readonly Dictionary<Type, (List<GroupEntry> groups, List<ButtonEntry> buttons)> _cache =
			new Dictionary<Type, (List<GroupEntry>, List<ButtonEntry>)>();

		[SerializeField]
		private List<GroupEntry> _groups;

		private List<ButtonEntry> _buttons;

		private void OnEnable()
		{
			if (ReferenceEquals(null, serializedObject.targetObject))
				return;
			
			if (!serializedObject.isEditingMultipleObjects &&
			    _cache.TryGetValue(serializedObject.targetObject.GetType(), out var cacheEntry))
			{
				_groups = cacheEntry.groups;
				_buttons = cacheEntry.buttons;
				return;
			}

			// search groups
			if (_groups == null)
				_groups = new List<GroupEntry>();

			var iterator = serializedObject.GetIterator();
			for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
			{
				if ("m_Script" == iterator.propertyPath)
					continue;

				var field = GetFieldFromProperty(iterator);
				var attribute = field?.GetCustomAttribute<GroupAttribute>();

				if (attribute == null)
					continue;

				var groupName = attribute.Name;
				_groups.Add(new GroupEntry(iterator.Copy(), groupName));
			}

			// search buttons
			var type = serializedObject.targetObject.GetType();
			_buttons = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
				.Select(m => (m, m.GetCustomAttribute<ButtonAttribute>()))
				.Where(m => m.Item2 != null)
				.Select(m => new ButtonEntry(m.Item1, m.Item2.ButtonName, m.Item2.OnlyOnPlayMode))
				.ToList();


			if (!serializedObject.isEditingMultipleObjects)
			{
				if (_groups.Count <= 0)
					_groups = null;
				if (_buttons.Count <= 0)
					_buttons = null;
				_cache.Add(serializedObject.targetObject.GetType(), (_groups, _buttons));
			}
		}

		private void OnDisable()
		{
			_groups = null;
			_buttons = null;
		}

		public override void OnInspectorGUI()
		{
			DrawMain();
			DrawButtons();
		}

		private void DrawMain()
		{
			if (_groups == null || _groups.Count <= 0)
			{
				DrawDefaultInspector();
				return;
			}

			if (ReferenceEquals(null, serializedObject.targetObject))
				return;
			
			serializedObject.Update();
			var iterator = serializedObject.GetIterator();
			var groupIndex = 0;
			var display = true;
			for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
			{
				if (groupIndex < _groups.Count)
				{
					var lookupEntry = _groups[groupIndex];
					if (SerializedProperty.EqualContents(iterator, lookupEntry.Property))
					{
						EditorGUILayout.Space();
						if (string.IsNullOrEmpty(lookupEntry.GroupName))
						{
							display = true;
						}
						else
						{
							
							var style = EditorStyles.foldout;
							var prevStyle = style.fontStyle;
							style.fontStyle = FontStyle.Bold;
							display = EditorGUILayout.Foldout(lookupEntry.Display,lookupEntry.GroupName, style);
							style.fontStyle = prevStyle;
							lookupEntry.Display = display;
						}

						groupIndex++;
					}
				}

				if (!display)
					continue;

				using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
					EditorGUILayout.PropertyField(iterator, true);
			}

			serializedObject.ApplyModifiedProperties();
		}

		protected void DrawButtons()
		{
			if (_buttons == null || _buttons.Count <= 0)
				return;

			EditorGUILayout.Space();

			for (var i = 0; i < _buttons.Count; i++)
			{
				var buttonEntry = _buttons[i];
				using (new EditorGUI.DisabledScope(buttonEntry.OnlyOnPlayMode && !Application.isPlaying))
				{
					if (GUILayout.Button(buttonEntry.ButtonName))
					{
						try
						{
							if (buttonEntry.Method.IsStatic)
								buttonEntry.Method.Invoke(null,null);
							else
							{
								if (serializedObject.isEditingMultipleObjects)
								{
									foreach (var obj in serializedObject.targetObjects)
									{
										buttonEntry.Method.Invoke(obj, null);
									}
								}
								else
								{
									buttonEntry.Method.Invoke(serializedObject.targetObject, null);
								}
							}
						}
						catch (AmbiguousMatchException e)
						{
							Debug.LogException(e);
						}
						catch (ArgumentException e)
						{
							Debug.LogException(e);
						}
						catch (NullReferenceException e)
						{
							Debug.LogException(e);
						}
					}
				}
			}
		}

		private static FieldInfo GetFieldFromProperty(SerializedProperty property)
		{
			var type = property.serializedObject.targetObject.GetType();
			return type.GetField(property.propertyPath);
		}

		[Serializable]
		public class GroupEntry
		{
			public SerializedProperty Property;
			public string GroupName;
			public bool Display;

			public GroupEntry(SerializedProperty property, string groupName)
			{
				Property = property;
				GroupName = groupName;
				Display = true;
			}
		}

		public class ButtonEntry
		{
			public MethodInfo Method;
			public string ButtonName;
			public bool OnlyOnPlayMode;

			public ButtonEntry(MethodInfo method, string buttonName, bool onlyOnPlayMode)
			{
				Method = method;
				ButtonName = buttonName;
				OnlyOnPlayMode = onlyOnPlayMode;
			}
		}
	}
}