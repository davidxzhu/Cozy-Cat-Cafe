using System;

namespace Plugins.CloudCanards.Inspector
{
	/// <summary>
	/// Marks where reflection is used to access potentially dangerous contents
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor)]
	public class InternalReflectionAttribute : Attribute
	{
		
	}
}