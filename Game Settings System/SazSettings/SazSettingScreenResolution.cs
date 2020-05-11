using UnityEngine;

namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingScreenResolution
	{
		public static string CurrentScreenResolution()
		{
			return ConvertResolution(Screen.currentResolution);
		}
		
		public static string[] ScreenResolutionOptions()
		{
			var resolutions = Screen.resolutions;
			var newResolutions = new string[resolutions.Length];

			for (var x = 0; x < newResolutions.Length; x++)
				newResolutions[x] = ConvertResolution(resolutions[x]);

			return newResolutions;
		}

		public static Resolution ConvertResolution(string option)
		{
			var split = option.Split('x');
			return new Resolution {width = int.Parse(split[0]), height = int.Parse(split[1])};
		}

		public static string ConvertResolution(Resolution resolution)
		{
			return resolution.width + "x" + resolution.height;
		}
	}
}