using UnityEngine;

namespace Plugins.CloudCanards.Inspector
{
	public class FunctionButtonAttribute : PropertyAttribute
	{
		public readonly string FunctionName;
		public readonly string ButtonName;
		public bool OnlyDuringPlayMode { get; set; } = false;
		public bool PlaceAboveField { get; set; } = false;

		public FunctionButtonAttribute(string functionName) : this(functionName, functionName)
		{
		}

		public FunctionButtonAttribute(string functionName, string buttonName)
		{
			FunctionName = functionName;
			ButtonName = buttonName;
		}
	}
}