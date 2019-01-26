using System;
using UnityEngine;

namespace Plugins.CloudCanards.Inspector
{
	/// <summary>
	/// Using this attribute disables inspector editing of the field
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ShowOnlyAttribute : PropertyAttribute
	{
	}
}