using UnityEngine;

namespace Plugins.CloudCanards.Inspector
{
	/// <summary>
	/// If string, cannot be null or whitespace
	/// If array, cannot be empty
	/// </summary>
	public class NotEmptyAttribute : PropertyAttribute
	{
		public string Message { get; set; } = "Cannot be empty!";
	}
}