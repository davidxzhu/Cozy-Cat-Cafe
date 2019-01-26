using UnityEngine;

namespace Plugins.CloudCanards.Inspector
{
	public class RequiredAttribute : PropertyAttribute
	{
		public string Message { get; set; } = "Cannot be null!";

		public RequiredAttribute()
		{
		}

		public RequiredAttribute(string message)
		{
			Message = message;
		}
	}
}