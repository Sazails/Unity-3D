using UnityEngine;

namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingShadowResolution
	{
		public static readonly string[] shadowResolutionOptions =
		{
			"Low",
			"Medium",
			"High",
			"Very High",
		};

		public static ShadowResolution ConvertShadowResolution(string option)
		{
			var options = shadowResolutionOptions;

			if (option == options[0])
				return ShadowResolution.Low;

			if (option == options[1])
				return ShadowResolution.Medium;

			if (option == options[3])
				return ShadowResolution.VeryHigh;

			return ShadowResolution.High;
		}
	}
}