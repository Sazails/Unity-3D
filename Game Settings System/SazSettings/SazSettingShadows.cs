using UnityEngine;

namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingShadows
	{
		public static readonly string[] shadowOptions =
		{
			"Disabled",
			"Hard",
			"Hard and Soft",
		};

		public static ShadowQuality ConvertShadowResolution(string option)
		{
			var options = shadowOptions;

			if (option == options[0])
				return ShadowQuality.Disable;

			if (option == options[1])
				return ShadowQuality.HardOnly;

			return ShadowQuality.All;
		}
	}
}