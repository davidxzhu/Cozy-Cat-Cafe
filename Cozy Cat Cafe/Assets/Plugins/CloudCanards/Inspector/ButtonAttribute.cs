using System;

namespace Plugins.CloudCanards.Inspector
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class ButtonAttribute : Attribute
	{
		public readonly string ButtonName;
		public bool OnlyOnPlayMode { get; set; } = false;

		public ButtonAttribute(string buttonName)
		{
			ButtonName = buttonName;
		}
	}
}