using System;

namespace Plugins.CloudCanards.Inspector
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class GroupAttribute : Attribute
	{
		public readonly string Name;

		public GroupAttribute(string name)
		{
			Name = name;
		}
	}
}